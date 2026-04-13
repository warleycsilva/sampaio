using System.Threading.Tasks;
using Sampaio.Domain.EmailsViewModels;

namespace Sampaio.Domain.Contracts.Infra
{
    public interface IJobsService
    {
        Task CheckApiStatus();
        Task SendErrorAlertMail();
        void EnqueueSendErrorAlertMail();
        void EnqueueEnvioEmailConfirmacaoCompra(ConfirmacaoCompraEmailVm vm);
        Task CheckPendingPaymentStatus();
    }
}