using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.RequestingDepartmen;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;

namespace vazaef.sazmanyar.Application.Contracts
{
    public interface IRequestingDepartmenService
    {
        Task<IEnumerable<GetAllRequestingDepartmenDto>> GetAllAsync();
        Task<GetByIdRequestingDepartmenDto> GetByIdAsync(long id);
        Task AddAsync(AddRequestingDepartmenDto dto);
        Task UpdateAsync(UpdateRequestingDepartmenDto dto);
        Task DeleteAsync(long id);
    }
}
