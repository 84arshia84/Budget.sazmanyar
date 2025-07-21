using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.PaymentMethod;

namespace vazaef.sazmanyar.Application.Contracts
{
    public interface IPaymentMethodService
    {
        Task<List<PaymentMethodDto>> GetAllAsync();
        Task<PaymentMethodDto> GetByIdAsync(long id);
        Task CreateAsync(CreatePaymentMethodDto dto);
        Task UpdateAsync(long id, CreatePaymentMethodDto dto);
        Task DeleteAsync(long id);
    }
}
