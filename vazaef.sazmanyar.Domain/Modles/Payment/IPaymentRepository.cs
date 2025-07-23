using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.Payment
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task DeleteAsync(long id);
        Task<Payment> GetByIdAsync(long id);
        Task<List<Payment>> GetAllAsync();
        Task<decimal> GetTotalPaidByAllocationAsync(long allocationId);

    }
}

