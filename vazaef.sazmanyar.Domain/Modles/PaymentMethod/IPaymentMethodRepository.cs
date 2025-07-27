using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.PaymentMethod
{
    // اینترفیس برای مدیریت روش‌های پرداخت
    public interface IPaymentMethodRepository
    {
        Task<List<PaymentMethod>> GetAllAsync(); // دریافت همه روش‌های پرداخت
        Task<PaymentMethod> GetByIdAsync(long id); // دریافت روش خاص با شناسه
        Task AddAsync(PaymentMethod method); // افزودن روش پرداخت جدید
        Task UpdateAsync(PaymentMethod method); // به‌روزرسانی اطلاعات روش پرداخت
        Task DeleteAsync(long id); // حذف روش پرداخت با شناسه
    }
}