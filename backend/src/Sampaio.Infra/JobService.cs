using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Hangfire;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.EmailsViewModels;
using Sampaio.Domain.Entities;
using Sampaio.Integrations.Pagarme;
using Sampaio.Shared.Config;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Mail;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.Utils;

namespace Sampaio.Infra
{
    public class JobService : IJobsService
    {
        private readonly ILogger _logger;
        private readonly IMailService _mailService;
        private readonly IViewRenderService _viewRenderService;
        private readonly BaseHttpClient _baseHttpClient;
        private readonly AppConfig _appConfig;
        private readonly IViagemPagamentoRepository _pagamentoRepository;
        private readonly IViagemRepository _viagemRepository;
        private readonly IUnitOfWork _uow;
        private readonly PagarmeClient _pagarmeClient;

        public JobService(ILogger logger,
            IMailService mailService,
            IViewRenderService viewRenderService,
            BaseHttpClient baseHttpClient,
            IViagemPagamentoRepository pagamentoRepository,
            IViagemRepository viagemRepository,
            PagarmeClient pagarmeClient,
            IUnitOfWork uow,
            AppConfig appConfig)
        {
            _logger = logger;
            _mailService = mailService;
            _viewRenderService = viewRenderService;
            _baseHttpClient = baseHttpClient;
            _pagamentoRepository = pagamentoRepository;
            _viagemRepository = viagemRepository;
            _pagarmeClient = pagarmeClient;
            _uow = uow;
            _appConfig = appConfig;
        }

        public async Task CheckApiStatus()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{_appConfig.BaseUrl}/api/status"))
            {
                var response = await _baseHttpClient.Client.SendAsync(request);
            }
        }

        public void EnqueueEnvioEmailConfirmacaoCompra(ConfirmacaoCompraEmailVm vm)
        {
            BackgroundJob.Enqueue(() => SendConfirmacaoCompraEmail(vm));
        }

        public async Task SendConfirmacaoCompraEmail(ConfirmacaoCompraEmailVm vm)
        {
            try
            {
                vm.Assunto = $"Confirmação de compra SampaioTur";
                var html = await _viewRenderService.RenderToStringAsync($"Emails/ConfirmacaoCompra", vm);

                _mailService.Send(vm.Email, vm.Assunto, html, true, cc: new List<string>()
                {
                    "contato@sampaioturismo.com"
                });
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }


        public void EnqueueSendErrorAlertMail()
        {
            BackgroundJob.Enqueue(() => SendErrorAlertMail());
        }

        public async Task SendErrorAlertMail()
        {
            try
            {
                var vm = new
                {
                    Email = "warleycesar10@hotmail.com",
                    Subject = "Error",
                };
                var html = await _viewRenderService.RenderToStringAsync($"Emails/ErrorAlert", vm);

                _mailService.Send(vm.Email, vm.Subject, html, true);
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }
        
        public async Task CheckPendingPaymentStatus()
        {
            try
            {
                if (!await _pagamentoRepository.AnyAsync(p => p.PagamentoStatus == EPaymentStatus.Pending
                                                              && p.CreatedAt >= DateTime.UtcNow.AddDays(-1)))
                    return;

                var includes = new IncludeHelper<ViagemPagamento>()
                    .Include(pagamento => pagamento.Viagem)
                    .Include(pagamento => pagamento.Passageiros)
                    .Includes;
                
                var pagamentos = _pagamentoRepository
                    .List(
                        p => p.PagamentoStatus == EPaymentStatus.Pending 
                             && p.CreatedAt >= DateTime.UtcNow.AddDays(-1)
                        , includes: includes);

                await _pagarmeClient.SetPaymentClientToken();

                var paraAtualizar = new List<ViagemPagamento>();
                var paraNotificar = new List<ViagemPagamento>();

                foreach (var pagamento in pagamentos)
                {
                    var orderId = pagamento?.IdTransacao;
                    var currentStatus = (EPaymentStatus)pagamento?.PagamentoStatus;

                    try{
                        var order = await _pagarmeClient.PaymentClient.GetOrderAsync(orderId);
                        pagamento.AtualizarStatus(order.Status);
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e);
                        continue;
                    }

                    if (pagamento.PagamentoStatus == currentStatus) continue;
                    
                    paraAtualizar.Add(pagamento);
                    
                    if(pagamento.PagamentoStatus == EPaymentStatus.Approved) 
                        paraNotificar.Add(pagamento);
                }
                
                if (paraAtualizar.Any())
                {
                    _pagamentoRepository.UpdateRange(paraAtualizar);
                    if ((await _uow.SaveChangesAsync() > 0) && paraNotificar.Any())
                    {
                        foreach (var pagamento in paraNotificar)
                        {
                            var passageiros = new List<PassageiroConfirmacaoAssentoEmailVm>();
                            foreach (var pagamentoPassageiro in pagamento.Passageiros)
                                passageiros.Add(new PassageiroConfirmacaoAssentoEmailVm()
                                {
                                    Assento = pagamentoPassageiro.Assento,
                                    Nome = pagamentoPassageiro.Nome,
                                    Documento = pagamentoPassageiro.Documento
                                });

                            if (pagamento.PagamentoStatus == EPaymentStatus.Approved)
                            {
                                EnqueueEnvioEmailConfirmacaoCompra(new ConfirmacaoCompraEmailVm
                                {
                                    Nome = pagamento.Passageiros?.FirstOrDefault(p => p.Comprador)?.Nome,
                                    Cpf = pagamento.Cpf,
                                    Email = pagamento.Email,
                                    ValorTotal = pagamento.Viagem.Preco * passageiros.Count(),
                                    Origem = pagamento.Viagem.Origem,
                                    Destino = pagamento.Viagem.Destino,
                                    DataPartida = pagamento.Viagem.DataPartida,
                                    Assunto = string.Empty,
                                    Passageiros = passageiros,
                                    Observacoes = pagamento.Viagem.Observacoes,
                                    FormaPagamento = string.IsNullOrEmpty(pagamento.PixQrCode) ? "Cartão de crédito" : "Pix"
                                });
                            }
                        }
                    }
                }

               

            }
            catch (Exception e)
            {
                try
                {
                    _mailService.Send("warley.avenger@gmail.com", "Erro no serviço",
                        $"Falha ao processar pagamentos: {JsonUtils.Serialize(e)}"
                        , false);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }
    }
}