using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.PaymentMethod;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    // ریپازیتوری مدیریت روش‌های پرداخت (PaymentMethod)
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly AppDbContext _context;

        public PaymentMethodRepository(AppDbContext context)
        {
            _context = context;
        }

        // دریافت همه روش‌های پرداخت
        public async Task<List<PaymentMethod>> GetAllAsync() =>
            await _context.PaymentMethods.ToListAsync();

        // دریافت روش پرداخت بر اساس شناسه
        public async Task<PaymentMethod> GetByIdAsync(long id) =>
            await _context.PaymentMethods.FindAsync(id);

        // افزودن روش پرداخت جدید
        public async Task AddAsync(PaymentMethod method)
        {
            _context.PaymentMethods.Add(method);
            await _context.SaveChangesAsync();
        }

        // به‌روزرسانی روش پرداخت موجود
        public async Task UpdateAsync(PaymentMethod method)
        {
            _context.PaymentMethods.Update(method);
            await _context.SaveChangesAsync();
        }

        // حذف روش پرداخت در صورت وجود
        public async Task DeleteAsync(long id)
        {
            var method = await _context.PaymentMethods.FindAsync(id);
            if (method != null)
            {
                _context.PaymentMethods.Remove(method);
                await _context.SaveChangesAsync();
            }
        }
    }
}