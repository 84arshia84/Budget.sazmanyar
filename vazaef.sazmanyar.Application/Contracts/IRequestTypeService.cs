using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.RequestType;
using vazaef.sazmanyar.Domain.Modles.RequestType;

namespace vazaef.sazmanyar.Application.Contracts
{
    public interface IRequestTypeService
    {
        Task<IEnumerable<GetAllRequestTypeDto>> GetAllAsync();
        Task<GetByIdRequestTypeDto> GetByIdAsync(long id);
        Task AddAsync(AddRequestTypeDto dto);
        Task UpdateAsync(UpdateRequestTypeDto dto);
        Task DeleteAsync(long id);
    }
}
