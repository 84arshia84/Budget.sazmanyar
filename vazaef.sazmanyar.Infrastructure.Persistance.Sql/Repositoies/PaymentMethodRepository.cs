using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.PaymentMethod;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly AppDbContext _context;

        public PaymentMethodRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentMethod>> GetAllAsync() =>
            await _context.PaymentMethods.ToListAsync();

        public async Task<PaymentMethod> GetByIdAsync(long id) =>
            await _context.PaymentMethods.FindAsync(id);

        public async Task AddAsync(PaymentMethod method)
        {
            _context.PaymentMethods.Add(method);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PaymentMethod method)
        {
            _context.PaymentMethods.Update(method);
            await _context.SaveChangesAsync();
        }

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
