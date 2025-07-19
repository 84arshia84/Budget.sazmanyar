using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.RequestingDepartmen;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;

namespace vazaef.sazmanyar.Application.Services
{
    public class RequestingDepartmenService : IRequestingDepartmenService
    {
        private readonly IRequestingDepartmenRepository _repository;

        public RequestingDepartmenService(IRequestingDepartmenRepository repository)        
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetAllRequestingDepartmenDto>> GetAllAsync()
        {
            var departments = await _repository.GetAllAsync();
            return departments.Select(d => new GetAllRequestingDepartmenDto
            {
                Id = d.Id,
                Description = d.Description
            });
        }

        public async Task<GetByIdRequestingDepartmenDto> GetByIdAsync(long id)
        {
            var department = await _repository.GetByIdAsync(id);
            if (department == null)
                return null;

            return new GetByIdRequestingDepartmenDto
            {
                Id = department.Id,
                Description = department.Description
            };
        }

        public async Task AddAsync(AddRequestingDepartmenDto dto)
        {
            var entity = new RequestingDepartmen
            {
                Description = dto.Description
            };

            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateRequestingDepartmenDto dto)
        {
            var entity = new RequestingDepartmen
            {
                Id = dto.Id,
                Description = dto.Description
            };

            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
