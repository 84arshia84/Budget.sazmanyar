using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.Payment;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    // ریپازیتوری مدیریت پرداخت‌ها (Payment)
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        // افزودن پرداخت جدید
        public async Task AddAsync(Payment payments)
        {
            await _context.Payments.AddAsync(payments);
            await _context.SaveChangesAsync();
        }

        // به‌روزرسانی پرداخت موجود
        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        // حذف پرداخت بر اساس شناسه
        public async Task DeleteAsync(long id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }

        // دریافت پرداخت بر اساس شناسه
        public async Task<Payment> GetByIdAsync(long id)
        {
            return await _context.Payments.FindAsync(id);
        }

        // دریافت همه پرداخت‌ها به صورت لیست
        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        // محاسبه مجموع مبلغ پرداخت شده برای تخصیص خاص
        public async Task<decimal> GetTotalPaidByAllocationAsync(long allocationId)
        {
            return await _context.Payments
                .Where(p => p.AllocationId == allocationId)
                .SumAsync(p => p.PaymentAmount); // جمع مبلغ پرداختی برای آن تخصیص
        }
    }
}
