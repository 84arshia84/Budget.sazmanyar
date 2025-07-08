using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.fundingSource;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;

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
            _context.FundingSources.Update(fundingSource);
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
