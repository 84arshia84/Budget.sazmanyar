using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.RequestType;

namespace vazaef.sazmanyar.Application.Contracts
{
    public interface IRequestTypeService
    {
        Task<IEnumerable<RequestType>> GetAllRequestTypesAsync();
        Task<RequestType> GetRequestTypeByIdAsync(long id);
        Task UpdateAsync(RequestType requestType);
        Task DeleteRequestTypeAsync(long id);
        Task AddAsync(RequestType requestType);
    }
}
