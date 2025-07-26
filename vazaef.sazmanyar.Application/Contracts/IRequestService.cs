using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.Request;
using vazaef.sazmanyar.Domain.Modles.Request;

namespace vazaef.sazmanyar.Application.Contracts
{
    public interface IRequestService
    {
       
        Task<GetRequestByIdDto> GetByIdAsync(long id);
        Task AddAsync(CreateRequestDto dto);
        Task<bool> UpdateAsync(long id, EditRequestDto dto);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<GetAllRequestDto>> GetAllWithTotalBudgetAsync();
        Task<IEnumerable<GetRequestByIdsDto>> GetByIdsAsync(IEnumerable<long> ids);
        Task<List<RequestDto>> GetAllWithBudgetEstimationAsync();
    }
}
