using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.RequestingUnit
{
    public interface IRequestingDepartmenRepository
    {
        Task<IEnumerable<RequestingDepartmen>> GetAllAsync();
        Task<RequestingDepartmen> GetByIdAsync(long id);
        Task AddAsync(RequestingDepartmen entity);
        Task UpdateAsync(RequestingDepartmen entity);
        Task DeleteAsync(long id);
    }
}
