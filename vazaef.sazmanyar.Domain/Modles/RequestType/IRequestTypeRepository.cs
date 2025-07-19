using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.RequestType
{
    public interface IRequestTypeRepository
    {
        Task<IEnumerable<RequestType>> GetAllAsync();
        Task<RequestType> GetByIdAsync(long id);
        Task AddAsync(RequestType entity);
        Task UpdateAsync(RequestType entity);
        Task DeleteAsync(long id);
    }
}
