using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.Payment;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Payment payments)
        {
            await _context.Payments.AddAsync(payments);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Payment> GetByIdAsync(long id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<decimal> GetTotalPaidByAllocationAsync(long allocationId)
        {
            // مجموع مبالغ پرداخت‌شده برای تخصیص مشخص
            return await _context.Payments
                .Where(p => p.AllocationId == allocationId)
                .SumAsync(p => p.PaymentAmount);
        }
    }
}
