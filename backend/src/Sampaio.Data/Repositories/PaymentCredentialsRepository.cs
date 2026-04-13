using PayRentee.Domain.Contracts.Repositories;
using PayRentee.Domain.Entities;

namespace PayRentee.Data.Repositories
{
    public class PaymentCredentialsRepository : Repository<PaymentCredentials>,IPaymentCredentialsRepository
    {
        public PaymentCredentialsRepository(DataContext context) : base(context)
        {
        }
    }
}