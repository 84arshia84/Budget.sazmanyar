using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.Allocation;

namespace vazaef.sazmanyar.Application.Contracts
{
    public interface IAllocationService
    {
        Task AddAsync(CreateAllocationDto dto);

        Task UpdateAsync(long id, UpdateAllocationDto dto);
        Task DeleteAsync(long id);
        Task<AllocationDto?> GetByIdAsync(long id);
        Task<List<AllocationDto>> GetAllAsync();
    }
}
