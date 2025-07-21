using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.Allocation
{
    public interface IAllocationRepository
    {
        Task<IEnumerable<Allocation>> GetAllAsync();
        Task<Allocation?> GetByIdAsync(long id);
        Task AddAsync(Allocation allocation);
        Task UpdateAsync(Allocation allocation);
        Task DeleteAsync(long id);
    }
}
