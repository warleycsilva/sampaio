using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.EmailsViewModels;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Projections.Viagens;
using Sampaio.Domain.Queries.Passageiro;
using Sampaio.Domain.Queries.Viagens;
using Sampaio.Domain.ViewModels.Viagens;
using Sampaio.Integrations.Pagarme;
using Sampaio.Shared.Config;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Paging;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.QueryHandlers
{
    public class ViagemQueryHandler : BaseQueryHandler,
        IRequestHandler<GetViagensQuery, PagedList<ViagemVm>>,
        IRequestHandler<GetPassageiroViagemByEmailQuery, PagedList<ViagemVm>>,
        IRequestHandler<GetLastPassageiroViagemByEmailQuery, ViagemPagamentoVm>,
        IRequestHandler<GetViagemByIdQuery, ViagemVm>
    {
        private readonly IViagemRepository _viagemRepository;
        private readonly IViagemPagamentoRepository _pagamentoRepository;
        private readonly IPassageiroRepository _passageiroRepository;
        private readonly IJobsService _jobsService;
        private readonly PagarmeConfig _pagarmeConfig;
        private readonly PagarmeClient _pagarmeClient;
        private readonly ILogger _logger;

        public ViagemQueryHandler(IViagemRepository viagemRepository,
            IViagemPagamentoRepository pagamentoRepository,
            IPassageiroRepository passageiroRepository,
            IDomainNotification notifications,
            PagarmeConfig pagarmeConfig
            , ILogger logger
            , PagarmeClient pagarmeClient,
            IJobsService jobsService,
            IUnitOfWork uow = null) :
            base(notifications, uow)
        {
            _pagarmeConfig = pagarmeConfig;
            _pagarmeClient = pagarmeClient;
            _jobsService = jobsService;
            _logger = logger;
            _viagemRepository = viagemRepository;
            _pagamentoRepository = pagamentoRepository;
            _passageiroRepository = passageiroRepository;
        }

        public async Task<PagedList<ViagemVm>> Handle(GetViagensQuery request, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Viagem>()
                .Include(x => x.ViagemPagamentos.Select(v => v.Passageiros))
                .Includes;
            // request.Filter.PageSize = 9999;
            var where = _viagemRepository.Where(request.Filter);

            var count = await _viagemRepository.CountAsync(where);

            var viagens = _viagemRepository
                .ListAsNoTracking(where, includes: includes)
                .OrderByDescending(x => x.DataPartida)?
                .ToVm()
                .Skip(request.Filter.PageSize * (request.Filter.PageIndex - 1))
                .Take(request.Filter.PageSize)
                .ToList();

            foreach (var viagem in viagens)
            {
                viagem.VagasVendidas = _passageiroRepository.Count(p => p.ViagemId == viagem.Id
                                                                        && p.Pagamento.PagamentoStatus ==
                                                                        EPaymentStatus.Approved);
            }

            return new PagedList<ViagemVm>(viagens, count, request.Filter.PageSize);
        }

        public async Task<ViagemVm> Handle(GetViagemByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Viagem>()
                .Include(x => x.Passageiros)
                .Include(x => x.ViagemPagamentos)
                .Include(x => x.ViagemPagamentos.Select(v => v.Passageiros))
                .Includes;
            var viagem = (await _viagemRepository.FindAsyncAsNoTracking(v => v.Id == request.Id, includes))?.ToVm();

            if (viagem is null) Notifications.Handle("Viagem não encontrada (ID).");

            viagem.AssentosOcupados = _passageiroRepository
                .ListAsNoTracking(p =>
                    p.ViagemId == request.Id && p.Pagamento.PagamentoStatus == EPaymentStatus.Approved)
                .Select(p => p.Assento)
                .ToList();

            viagem.AssentosDisponiveis = Enumerable.Range(1, viagem.QtdVagas)
                .Where(x => !viagem.AssentosOcupados.Contains(x))
                .ToList();

            var passageirosPagantes = new List<PassageiroVm>();
            foreach (var viagemPassageiro in viagem.Passageiros)
            {
                var compra = viagem
                    .ViagemPagamentos
                    .FirstOrDefault(p =>
                        p.Passageiros.Any(passageiro => passageiro.Id == viagemPassageiro.Id)
                        && (p.PagamentoStatus == EPaymentStatus.Approved
                            || p.PagamentoStatus == EPaymentStatus.Refunded)
                    );

                if (compra is null)
                    continue;

                viagemPassageiro.StatusPagamento = compra.PagamentoStatus;
                switch (viagemPassageiro.StatusPagamento)
                {
                    case EPaymentStatus.Approved:
                        viagemPassageiro.StatusPagamentoDescricao = "Aprovado";
                        break;
                    case EPaymentStatus.Refunded:
                        viagemPassageiro.StatusPagamentoDescricao = "Estornado";
                        break;
                    default:
                        viagemPassageiro.StatusPagamentoDescricao = "Não encontrado";
                        break;
                }

                viagemPassageiro.CpfComprador = compra.Cpf;
                viagemPassageiro.EmailComprador = compra.Email;
                if (viagemPassageiro.Comprador)
                {
                    viagemPassageiro.ValorPago = compra.ValorTotal;
                    viagem.ValorTotalFaturado += compra.ValorTotal;
                    viagemPassageiro.DataDaCompra = compra.DataDaCompra;
                }

                passageirosPagantes.Add(viagemPassageiro);
            }

            viagem.ViagemPagamentos = viagem.ViagemPagamentos.Where(p => p.PagamentoStatus == EPaymentStatus.Approved);
            viagem.Passageiros = passageirosPagantes.OrderBy(p => p.StatusPagamento).ThenBy(p => p.Assento);
            return viagem;
        }

        public async Task<PagedList<ViagemVm>> Handle(GetPassageiroViagemByEmailQuery request,
            CancellationToken cancellationToken)
        {
            var where = _viagemRepository.Where(request.Filter);
            var count = await _viagemRepository.CountAsync(where);
            var result = _viagemRepository.ListAsNoTracking(where, request.Filter);
            return new PagedList<ViagemVm>(result.ToVm(), count, request.Filter.PageSize);
        }

        public async Task<ViagemPagamentoVm> Handle(GetLastPassageiroViagemByEmailQuery request,
            CancellationToken cancellationToken)
        {
            var pagamento = _pagamentoRepository.List(p => p.Email == request.Filter.Email, includes: p => p.Passageiros)
                .OrderByDescending(c => c.CreatedAt).FirstOrDefault();
            if (pagamento == null) Notifications.Handle("Pagamento não encontrado");
            if (pagamento?.PagamentoStatus != EPaymentStatus.Approved)
            {
                var orderId = pagamento?.IdTransacao;
                var currentStatus = (EPaymentStatus)pagamento?.PagamentoStatus;

                await _pagarmeClient.SetPaymentClientToken();

                var order = await _pagarmeClient.PaymentClient.GetOrderAsync(orderId);

                pagamento.AtualizarStatus(order.Status);
                if (pagamento.PagamentoStatus != EPaymentStatus.Approved)
                    _logger.Warning("SyncStatus " + JsonUtils.Serialize(pagamento));
                if (pagamento.PagamentoStatus != currentStatus)
                    if (!await CommitAsync())
                        Notifications.Handle(CommonMessages.ProblemSavindDataFriendly);
            }

            if (pagamento.PagamentoStatus == EPaymentStatus.Approved)
            {
                var viagem = await _viagemRepository.FindAsyncAsNoTracking(v => v.Id == pagamento.ViagemId);

                var passageiros = new List<PassageiroConfirmacaoAssentoEmailVm>();
                foreach (var pagamentoPassageiro in pagamento.Passageiros)
                    passageiros.Add(new PassageiroConfirmacaoAssentoEmailVm()
                    {
                        Assento = pagamentoPassageiro.Assento,
                        Nome = pagamentoPassageiro.Nome,
                        Documento = pagamentoPassageiro.Documento
                    });
                var vm = new ConfirmacaoCompraEmailVm
                {
                    Nome = pagamento.Passageiros?.FirstOrDefault(p => p.Comprador)?.Nome,
                    Cpf = pagamento.Cpf,
                    Email = pagamento.Email,
                    ValorTotal = viagem.Preco * passageiros.Count(),
                    Origem = viagem.Origem,
                    Destino = viagem.Destino,
                    DataPartida = viagem.DataPartida,
                    Assunto = string.Empty,
                    Passageiros = passageiros,
                    Observacoes = viagem.Observacoes,
                    FormaPagamento = !string.IsNullOrEmpty(pagamento.PixQrCode) ? "Pix" : "Cartão de crédito"
                };
                _jobsService.EnqueueEnvioEmailConfirmacaoCompra(vm);
            }

            return pagamento.ToVm();
        }
    }
}