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
        Task AddAsync(RequestType requestType);
        Task UpdateAsync(RequestType requestType);
        Task DeleteAsync(long id);
    }
}
