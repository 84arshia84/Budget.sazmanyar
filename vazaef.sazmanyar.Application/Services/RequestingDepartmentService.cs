using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;

namespace vazaef.sazmanyar.Application.Services
{
    public class RequestingDepartmentService : IRequestingDepartmentService
    {
        private readonly IRequestingDepartmentRepository _repository;

        public RequestingDepartmentService(IRequestingDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RequestingDepartment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<RequestingDepartment> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(RequestingDepartment requestingDepartment)
        {
            await _repository.AddAsync(requestingDepartment);
        }

        public async Task UpdateAsync(RequestingDepartment requestingDepartment)
        {
            await _repository.UpdateAsync(requestingDepartment);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
