using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.Payment;

namespace vazaef.sazmanyar.Application.Contracts
{
    // اینترفیس سرویس پرداخت‌ها
    public interface IPaymentService
    {
        Task CreateAsync(CreatePaymentDto dto); // ایجاد پرداخت جدید
        Task UpdateAsync(long id ,UpdatePaymentDto dto); // به‌روزرسانی اطلاعات پرداخت
        Task DeleteAsync(long id); // حذف پرداخت با شناسه
        Task<PaymentDto> GetByIdAsync(long id); // دریافت اطلاعات پرداخت با شناسه
        Task<List<PaymentDto>> GetAllAsync(); // دریافت لیست پرداخت‌ها
    }
}