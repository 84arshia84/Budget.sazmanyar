using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.Request;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _context;

        public RequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RequestEntity>> GetAllAsync() =>
            await _context.Requests
                .Include(r => r.ActionBudgetRequests)
                .ToListAsync();

        public async Task<RequestEntity> GetByIdAsync(long id) =>
    await _context.Requests
        .Include(r => r.ActionBudgetRequests) // ✅ این خط اضافه بشه
        .FirstOrDefaultAsync(r => r.Id == id);

        public async Task AddAsync(RequestEntity request)
        {
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RequestEntity request)
        {
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
            }
        }

    }
}
