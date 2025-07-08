using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.Request
{
    public interface IRequestRepository
    {
        Task<IEnumerable<Request>> GetAllAsync();
        Task<Request> GetByIdAsync(long id);
        Task AddAsync(Request request);
        Task UpdateAsync(Request request);
        Task DeleteAsync(long id);
    }
}