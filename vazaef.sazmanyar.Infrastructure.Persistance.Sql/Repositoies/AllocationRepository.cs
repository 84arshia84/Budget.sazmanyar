using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.Allocation;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    public class AllocationRepository : IAllocationRepository
    {
        private readonly AppDbContext _context;

        public AllocationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Allocation>> GetAllAsync()
        {
            return await _context.Allocations
                .Include(a => a.Request)
                .Include(a => a.Payments)
                .ToListAsync();
        }

        public async Task<Allocation?> GetByIdAsync(long id)
        {
            return await _context.Allocations
                .Include(a => a.Request)
                .Include(a => a.Payments)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Allocation allocation)
        {
            await _context.Allocations.AddAsync(allocation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Allocation allocation)
        {
            _context.Allocations.Update(allocation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var allocation = await _context.Allocations.FindAsync(id);
            if (allocation != null)
            {
                _context.Allocations.Remove(allocation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
