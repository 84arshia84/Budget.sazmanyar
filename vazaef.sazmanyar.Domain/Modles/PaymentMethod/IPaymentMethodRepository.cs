using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.PaymentMethod
{
    public interface IPaymentMethodRepository
    {
        Task<List<PaymentMethod>> GetAllAsync();
        Task<PaymentMethod> GetByIdAsync(long id);
        Task AddAsync(PaymentMethod method);
        Task UpdateAsync(PaymentMethod method);
        Task DeleteAsync(long id);
    }
}
