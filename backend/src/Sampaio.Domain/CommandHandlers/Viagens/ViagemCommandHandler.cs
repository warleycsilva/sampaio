using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestEase;
using Sampaio.Domain.Commands.ViagemPagamentos;
using Sampaio.Domain.Commands.Viagens;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.EmailsViewModels;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Projections.Viagens;
using Sampaio.Domain.Results.Viagens;
using Sampaio.Domain.ViewModels.Viagens;
using Sampaio.Integrations.Pagarme;
using Sampaio.Integrations.Pagarme.Models;
using Sampaio.Shared.Config;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Extensions;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.Structs;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.CommandHandlers.Viagens
{
    public class ViagemCommandHandler : BaseCommandHandler,
        IRequestHandler<CriarViagemCmd, ViagemBaseResult>,
        IRequestHandler<EditarViagemCmd, ViagemBaseResult>,
        IRequestHandler<CriarViagemPagamentoCmd, ViagemPagamentoBaseResult>,
        IRequestHandler<SyncStatusPagamentoCommand, ViagemPagamentoVm>,
        IRequestHandler<AlterarStatusAtivoViagemCmd, ViagemBaseResult>,
        IRequestHandler<EstornarViagemCmd, ViagemBaseResult>
    {
        private readonly IJobsService _jobsService;
        private readonly IViagemRepository _viagemRepository;
        private readonly IViagemPagamentoRepository _pagamentoRepository;
        private readonly IPassageiroRepository _passageiroRepository;
        private readonly PagarmeConfig _pagarmeConfig;
        private readonly PagarmeClient _pagarmeClient;
        private readonly ILogger _logger;

        public ViagemCommandHandler(IUnitOfWork uow
            , IDomainNotification notifications
            , IJobsService jobsService
            , IViagemRepository viagemRepository
            , IViagemPagamentoRepository pagamentoRepository
            , IPassageiroRepository passageiroRepository
            , PagarmeConfig pagarmeConfig
            , ILogger logger
            , PagarmeClient pagarmeClient) : base(uow, notifications)
        {
            _viagemRepository = viagemRepository;
            _pagamentoRepository = pagamentoRepository;
            _passageiroRepository = passageiroRepository;
            _pagarmeConfig = pagarmeConfig;
            _pagarmeClient = pagarmeClient;
            _jobsService = jobsService;
            _logger = logger;
        }

        public async Task<ViagemBaseResult> Handle(CriarViagemCmd request, CancellationToken cancellationToken)
        {
            var result = new ViagemBaseResult();

            var viagem = new Viagem(
                request.Origem,
                request.Destino,
                request.DataPartida,
                request.Preco,
                request.QtdVagas,
                request.Observacoes);

            await _viagemRepository.AddAsync(viagem);

            if (!await CommitAsync()) Notifications.Handle(CommonMessages.ProblemSavindDataFriendly);

            result.Message = "Viagem adicionada com sucesso";
            result.Success = true;
            result.Viagem = viagem.ToVm();

            return result;
        }

        public async Task<ViagemPagamentoBaseResult> Handle(CriarViagemPagamentoCmd request,
            CancellationToken cancellationToken)
        {
            var result = new ViagemPagamentoBaseResult();
            var comprador = request.Passageiros.FirstOrDefault(p => p.Comprador);

            if (!request.Passageiros.Any()) Notifications.Handle("Ao menos 1 passageiro deve ser informado.");

            comprador ??= request.Passageiros.FirstOrDefault();


            var viagem = await _viagemRepository.FindAsync(v => v.Id == request.ViagemId);
            if (viagem is null)
            {
                Notifications.Handle("Viagem não encontrada");
                return null;
            };

            if (viagem.QtdVagas == (await _passageiroRepository.CountAsync(p => p.ViagemId == viagem.Id && p.Pagamento.PagamentoStatus == EPaymentStatus.Approved)))
            {
                Notifications.Handle("Não há mais vagas disponíveis para essa viagem");
                return null;
            }

            foreach (var passageiro in request?.Passageiros)
            {
                var pago = await _pagamentoRepository.AnyAsync(pgto =>
                    (pgto.PagamentoStatus == EPaymentStatus.Approved || (pgto.PagamentoStatus == EPaymentStatus.Pending && pgto.CreatedAt >= DateTime.UtcNow.AddMinutes(-10))) &&
                    pgto.Passageiros.Any(p => p.ViagemId == request.ViagemId && p.Assento == passageiro.Assento));

                if (!pago) continue;
                Notifications.Handle($"O assento {passageiro.Assento.ToString()} já está ocupado.");
                return null;
            }

            var pagamento = new ViagemPagamento(
                request.ViagemId,
                request.Passageiros.Count,
                valorTotal: (request.Passageiros.Count * viagem.Preco),
                request.Email,
                request.Cpf);

            var phoneFormatted = comprador.Telefone
                .Replace("(", string.Empty)
                .Replace(")", string.Empty)
                .Replace("-", string.Empty)
                .Replace(" ", string.Empty)
                .Trim();

            if (phoneFormatted.Length < 9)
            {
                Notifications.Handle($"Verifique o telefone do passageiro: " + comprador.Nome);
                return null;
            }

            if (request.Cpf.Length == 10)
            {
                request.Cpf = "0" + request.Cpf;
            }
            if (request.Cpf.Length != 11)
            {
                Notifications.Handle($"Verifique se o CPF informado possui 11 digitos.");
                return null;
            }

            var orderRequest = new CreateOrderRequest();
            try
            {
                var customer = new CreateCustomerRequest
                {
                    Name = comprador.Nome,
                    Document = request.Cpf,
                    Type = "individual",
                    Email = request.Email,
                    Phones = new CreatePhonesRequest()
                    {
                        HomePhone = new CreatePhoneRequest()
                        {
                            CountryCode = "55",
                            Number = phoneFormatted[2..],
                            AreaCode = phoneFormatted[..2]
                        }
                    },
                };
                var items = new List<CreateOrderItemRequest>();
                items.Add(new CreateOrderItemRequest()
                {
                    Amount = (int)((viagem.Preco * 100) * request.Passageiros.Count),
                    Description = "SAMPAIOTUR " + viagem.Destino,
                    Quantity = 1,
                    Code = pagamento.Id.ToString()
                });

                var payments = new List<CreatePaymentRequest>();
                if (request.TipoPagamento == EPaymentType.Pix)
                {
                    payments.Add(new CreatePaymentRequest()
                    {
                        Amount = (int)((viagem.Preco * 100) * request.Passageiros.Count),
                        PaymentMethod = "pix",
                        Pix = new CreatePixPaymentRequest()
                        {
                            ExpiresIn = (int)TimeSpan.FromMinutes(10).TotalSeconds
                        }
                    });
                }
                else if (request.TipoPagamento == EPaymentType.Credit)
                {
                    if (request.CartaoCredito == null)
                    {
                        Notifications.Handle($"Verifique se preencheu os dados do cartão.");
                        return null;
                    }
                    if (string.IsNullOrEmpty(request.CartaoCredito.EnderecoCepCartao))
                    {
                        Notifications.Handle($"Verifique se preencheu o CEP. (Cartão de crédito");
                        return null;
                    }
                    if (string.IsNullOrEmpty(request.CartaoCredito.EnderecoEstadoCartao))
                    {
                        Notifications.Handle($"Verifique se preencheu o estado (ex.: MG). (Cartão de crédito");
                        return null;
                    }
                    if (string.IsNullOrEmpty(request.CartaoCredito.EnderecoBairroCartao))
                    {
                        Notifications.Handle($"Verifique se preencheu o bairro (ex.: MG). (Cartão de crédito");
                        return null;
                    }
                    if (string.IsNullOrEmpty(request.CartaoCredito.EnderecoCidadeCartao))
                    {
                        Notifications.Handle($"Verifique se preencheu a cidade. (Cartão de crédito");
                        return null;
                    }
                    if (string.IsNullOrEmpty(request.CartaoCredito.NumeroCartao))
                    {
                        Notifications.Handle($"Verifique se preencheu o número do endereço. (Cartão de crédito");
                        return null;
                    }
                    if (request.CartaoCredito.EnderecoCepCartao.Replace("-",string.Empty).Length != 8)
                    {
                        Notifications.Handle($"CEP deve ter 8 dígitos (sem traços ou pontos).");
                        return null;
                    }
                    payments.Add(new CreatePaymentRequest()
                    {
                        PaymentMethod = "credit_card",
                        CreditCard = new CreateCreditCardPaymentRequest
                        {
                            Installments = request.CartaoCredito.Parcelas,
                            StatementDescriptor = "SAMPAIOTUR",
                            Card = new CreateCardRequest
                            {
                                Number = request.CartaoCredito.NumeroCartao,
                                HolderName = request.CartaoCredito.NomeTitularCartao?.RemoveNumbersSpecialCharactersAndAccents(),
                                HolderDocument = request.CartaoCredito.DocTitularCartao,
                                ExpMonth = request.CartaoCredito.MesExpiracao,
                                ExpYear = request.CartaoCredito.AnoExpiracao,
                                Cvv = request.CartaoCredito.CodigoSegurancaCartao,
                                BillingAddress = new CreateAddressRequest()
                                {
                                    Street = request.CartaoCredito.EnderecoRuaCartao,
                                    Number = request.CartaoCredito.EnderecoNumeroCartao,
                                    ZipCode =request.CartaoCredito.EnderecoCepCartao,
                                    Neighborhood = request.CartaoCredito.EnderecoBairroCartao,
                                    City = request.CartaoCredito.EnderecoCidadeCartao,
                                    State = request.CartaoCredito.EnderecoEstadoCartao,
                                    Country = "BR"
                                }
                            },
                            Recurrence = false,
                            Capture = true,
                            OperationType = "auth_and_capture",
                        }
                    });
                    
                    customer.Address = new CreateAddressRequest
                    {
                        Street = request.CartaoCredito.EnderecoRuaCartao,
                        Number = request.CartaoCredito.EnderecoNumeroCartao,
                        ZipCode = request.CartaoCredito.EnderecoCepCartao,
                        Neighborhood = request.CartaoCredito.EnderecoBairroCartao,
                        City = request.CartaoCredito.EnderecoCidadeCartao,
                        State = request.CartaoCredito.EnderecoEstadoCartao,
                        Country = "BR"
                    };
                }

               orderRequest = new CreateOrderRequest()
                {
                    Customer = customer,
                    Items = items,
                    Payments = payments,
                    Code = pagamento.Id.ToString(),
                    Closed = true
                };

                orderRequest.Antifraud = null;
                orderRequest.AntifraudEnabled = false;
            }
            catch (Exception e)
            {
                _logger.Error("[CreatePayment] Erro ao criar pagamento", e);
                Notifications.Handle(
                    $"Verifique se preencheu o telefone dos passageiros e o CPF e nome do comprador corretamente");
                return null;
            }

            try
            {
                await _pagarmeClient.SetPaymentClientToken();
                var order = await _pagarmeClient.PaymentClient.CreateOrderAsync(orderRequest);
                if (order.Status == null)
                {
                    _logger.Warning("Pagamento falhou sem status" + JsonUtils.Serialize(order));
                    _logger.Warning(JsonUtils.Serialize(orderRequest));
                    Notifications.Handle($"Verifique se preencheu todos os dados de pagamento corretamente e tente novamente.");
                    return null;
                }

                pagamento.AtualizarStatus(order.Status);

                if (pagamento.PagamentoStatus != EPaymentStatus.Approved &&
                    pagamento.PagamentoStatus != EPaymentStatus.Pending)
                {
                    _logger.Warning($"FAILEDPGTO_{JsonUtils.Serialize(order)}");
                }

                pagamento.IdTransacao = order.Id;

                foreach (var passageiro in request?.Passageiros)
                {
                    await _passageiroRepository.AddAsync(new Passageiro(
                        request.ViagemId,
                        passageiro.Assento,
                        passageiro.Nome,
                        passageiro.Documento,
                        passageiro.Telefone,
                        passageiro.Comprador,
                        pagamento.Id));
                }

                if (request.TipoPagamento == EPaymentType.Pix)
                {
                    pagamento.PixQrCode = order.Charges.FirstOrDefault().LastTransaction.QrCode;
                    result.PixQrCode = order.Charges.FirstOrDefault().LastTransaction.QrCode;
                    await _pagamentoRepository.AddAsync(pagamento);

                    if (!await CommitAsync())
                    {
                        Notifications.Handle(CommonMessages.ProblemSavindDataFriendly);
                        return null;
                    }
                }
                else if (request.TipoPagamento == EPaymentType.Credit && pagamento.PagamentoStatus == EPaymentStatus.Approved)
                {
                    await _pagamentoRepository.AddAsync(pagamento);
                    if (!await CommitAsync())
                    {
                        Notifications.Handle(CommonMessages.ProblemSavindDataFriendly);
                        return null;
                    }
                    var passageirosToMail = new List<PassageiroConfirmacaoAssentoEmailVm>();
                    foreach (var pagamentoPassageiro in pagamento.Passageiros)
                        passageirosToMail.Add(new PassageiroConfirmacaoAssentoEmailVm()
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
                        ValorTotal = viagem.Preco * passageirosToMail.Count(),
                        Origem = viagem.Origem,
                        Destino = viagem.Destino,
                        DataPartida = viagem.DataPartida,
                        Assunto = string.Empty,
                        Passageiros = passageirosToMail,
                        Observacoes = viagem.Observacoes,
                        FormaPagamento = !string.IsNullOrEmpty(pagamento.PixQrCode) ? "Pix" : "Cartão de crédito"
                    };
                    _jobsService.EnqueueEnvioEmailConfirmacaoCompra(vm);
                }

                if (pagamento.PagamentoStatus != EPaymentStatus.Approved && pagamento.PagamentoStatus != EPaymentStatus.Pending)
                    if (order.Charges != null && order.Charges.Any(c => c.LastTransaction != null 
                                                                        && c.LastTransaction.GatewayResponse != null
                                                                        && c.LastTransaction.GatewayResponse.Errors
                                                                            .Any(error => error.Message.Contains("CPF"))))
                    {
                        Notifications.Handle("CPF Inválido, verifique o CPF informado e tente novamente.");
                        return null;
                    }

                result.Message = pagamento.PagamentoStatus == EPaymentStatus.Approved
                    ? "Pagamento realizado com sucesso"
                    : pagamento.PagamentoStatus == EPaymentStatus.Pending
                        ? "Solicitação recebida com sucesso, aguardando pagamento para reserva das passagens."
                        : "Pagamento recusado, verifique os dados informados e tente novamente";
                result.Success = true;
                result.Status = pagamento.PagamentoStatus;

            }
            catch (ApiException ex)
            {
                _logger.Warning(ex.Message);
                Notifications.Handle($"Erro ao efetuar o pagamento, verifique os dados informados e tente novamente.");
                return null;
            }
            return result;
        }

        public async Task<ViagemBaseResult> Handle(EditarViagemCmd request, CancellationToken cancellationToken)
        {
            var result = new ViagemBaseResult();

            var viagem = await _viagemRepository.FindAsync(v => v.Id == request.Id);

            viagem.Update(request);

            if (!await CommitAsync()) Notifications.Handle(CommonMessages.ProblemSavindDataFriendly);

            //TODO: ùltima prioridade: Enviar e-mail com todos os dados da viagem para o email do comprador - Testar com email;

            result.Message = "Viagem atualizada com sucesso";
            result.Success = true;
            result.Viagem = viagem.ToVm();

            return result;
        }

        public async Task<ViagemBaseResult> Handle(AlterarStatusAtivoViagemCmd request,
            CancellationToken cancellationToken)
        {
            var result = new ViagemBaseResult();

            var viagem = await _viagemRepository.FindAsync(v => v.Id == request.Id);

            if (viagem == null) Notifications.Handle("Viagem não encontrada");

            viagem.ToggleActiveStatus();

            if (!await CommitAsync()) Notifications.Handle(CommonMessages.ProblemSavindDataFriendly);

            result.Message = $"Viagem {(viagem.IsActive.Value ? "ativada" : "desativada")} com sucesso";
            result.Success = true;
            result.Viagem = viagem.ToVm();

            return result;
        }

        public async Task<ViagemPagamentoVm> Handle(SyncStatusPagamentoCommand request,
            CancellationToken cancellationToken)
        {
            var pagamento = await _pagamentoRepository.FindAsync(v => v.Id == request.PagamentoId, p => p.Passageiros);
            if (pagamento == null)
            {
                Notifications.Handle("Pagamento não encontrado");
                return null;
            }
            if (pagamento?.PagamentoStatus != EPaymentStatus.Approved)
            {
                var orderId = pagamento?.IdTransacao;
                var currentStatus = pagamento?.PagamentoStatus;

                await _pagarmeClient.SetPaymentClientToken();

                var order = await _pagarmeClient.PaymentClient.GetOrderAsync(orderId);

                pagamento.AtualizarStatus(order.Status);
                if (pagamento.PagamentoStatus != EPaymentStatus.Approved)
                    _logger.Warning("SyncStatus " + JsonUtils.Serialize(pagamento));
                if (pagamento.PagamentoStatus != EPaymentStatus.Pending)
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

        public async Task<ViagemBaseResult> Handle(EstornarViagemCmd request, CancellationToken cancellationToken)
        {
            var result = new ViagemBaseResult();

            var viagemPagamento =
                await _pagamentoRepository.FindAsync(v => v.Passageiros.Any(passageiro => passageiro.Id == request.Id));

            if (viagemPagamento == null)
            {
                Notifications.Handle("Pagamento não encontrado");
                return null;
            }

            viagemPagamento.ReembolsarPagamento();

            if (!await CommitAsync()) Notifications.Handle(CommonMessages.ProblemSavindDataFriendly);

            result.Message = $"Viagem reembolsada com sucesso";
            result.Success = true;

            return result;
        }
    }
}