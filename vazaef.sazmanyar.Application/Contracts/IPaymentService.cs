using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.Payment;

namespace vazaef.sazmanyar.Application.Contracts
{
    public interface IPaymentService
    {
        Task CreateAsync(CreatePaymentDto dto);
        Task UpdateAsync(UpdatePaymentDto dto);
        Task DeleteAsync(long id);
        Task<PaymentDto> GetByIdAsync(long id);
        Task<List<PaymentDto>> GetAllAsync();
    }
}
