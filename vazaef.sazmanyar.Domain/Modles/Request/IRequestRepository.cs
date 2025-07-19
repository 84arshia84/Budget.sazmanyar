using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.Request
{
    public interface IRequestRepository
    {
        //Task<IEnumerable<RequestEntity>> GetAllAsync();
        Task<RequestEntity> GetByIdAsync(long id);
        Task AddAsync(RequestEntity request);
        Task UpdateAsync(RequestEntity request);
        Task DeleteAsync(long id);
        Task<string> GetAllRequestsWithTotalBudgetJsonAsync();

    }
}