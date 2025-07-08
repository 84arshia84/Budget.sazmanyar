using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;

namespace vazaef.sazmanyar.Application.Contracts
{
    public interface IRequestingDepartmentService
    {
        Task<IEnumerable<RequestingDepartment>> GetAllAsync();
        Task<RequestingDepartment> GetByIdAsync(long id);
        Task AddAsync(RequestingDepartment requestingDepartment);
        Task UpdateAsync(RequestingDepartment requestingDepartment);
        Task DeleteAsync(long id);
    }
}
