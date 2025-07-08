using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.RequestingUnit
{
    public interface IRequestingDepartmentRepository
    {
        Task<IEnumerable<RequestingDepartment>> GetAllAsync();
        Task<RequestingDepartment> GetByIdAsync(long id);
        Task AddAsync(RequestingDepartment requestingDepartment);
        Task UpdateAsync(RequestingDepartment requestingDepartment);
        Task DeleteAsync(long id);
    }
}
