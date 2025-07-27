using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.PaymentMethod;

namespace vazaef.sazmanyar.Application.Contracts
{
    // اینترفیس سرویس روش‌های پرداخت
    public interface IPaymentMethodService
    {
        Task<List<PaymentMethodDto>> GetAllAsync(); // دریافت لیست همه روش‌های پرداخت
        Task<PaymentMethodDto> GetByIdAsync(long id); // دریافت اطلاعات روش پرداخت با شناسه
        Task CreateAsync(CreatePaymentMethodDto dto); // ایجاد روش پرداخت جدید
        Task UpdateAsync(long id, CreatePaymentMethodDto dto); // به‌روزرسانی روش پرداخت
        Task DeleteAsync(long id); // حذف روش پرداخت با شناسه
    }
}