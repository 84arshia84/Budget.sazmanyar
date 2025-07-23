using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.fundingSource;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;
using vazaef.sazmanyar.Domain.Modles.RequestType;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    public class FundingSourceRepository : IFundingSourceRepository
    {
        private readonly AppDbContext _context;

        public FundingSourceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FundingSource>> GetAllAsync()
        {
            return await _context.FundingSources.ToListAsync();
        }

        public async Task<FundingSource> GetByIdAsync(long id)
        {
            return await _context.FundingSources.FindAsync(id);
        }

        public async Task AddAsync(FundingSource fundingSource)
        {
            await _context.FundingSources.AddAsync(fundingSource);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FundingSource fundingSource)
        {
            var existing = await _context.FundingSources
                .Include(rt => rt.Requests)
                .FirstOrDefaultAsync(rt => rt.Id == fundingSource.Id);

            if (existing == null)
                throw new KeyNotFoundException($"RequestType with Id={fundingSource.Id} not found.");

            existing.Description = fundingSource.Description;
            if (fundingSource.Requests != null && fundingSource.Requests.Any())
            {
                existing.Requests.Clear();
                foreach (var req in fundingSource.Requests)
                {
                    existing.Requests.Add(req);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var fundingSource = await _context.FundingSources.FindAsync(id);
            if (fundingSource != null)
            {
                _context.FundingSources.Remove(fundingSource);
                await _context.SaveChangesAsync();
            }
        }
    }
}
