using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.Payment
{
    // اینترفیس برای عملیات پایگاه‌داده روی پرداخت‌ها
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment); // افزودن پرداخت جدید
        Task UpdateAsync(Payment payment); // به‌روزرسانی پرداخت
        Task DeleteAsync(long id); // حذف پرداخت با شناسه
        Task<Payment> GetByIdAsync(long id); // دریافت پرداخت با شناسه
        Task<List<Payment>> GetAllAsync(); // دریافت لیست تمام پرداخت‌ها
        Task<decimal> GetTotalPaidByAllocationAsync(long allocationId); // محاسبه مجموع پرداخت‌های انجام‌شده برای یک تخصیص خاص
    }
}