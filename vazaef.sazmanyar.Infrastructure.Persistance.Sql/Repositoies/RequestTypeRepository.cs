using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.RequestType;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    public class RequestTypeRepository : IRequestTypeRepository
    {
        private readonly AppDbContext _context;

        public RequestTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RequestType>> GetAllAsync()
        {
            return await _context.RequestTypes.ToListAsync();
        }

        public async Task<RequestType> GetByIdAsync(long id)
        {
            return await _context.RequestTypes.FindAsync(id);
        }

        public async Task AddAsync(RequestType requestType)
        {
            await _context.RequestTypes.AddAsync(requestType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RequestType requestType)
        {
            _context.RequestTypes.Update(requestType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var requestType = await _context.RequestTypes.FindAsync(id);
            if (requestType != null)
            {
                _context.RequestTypes.Remove(requestType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
