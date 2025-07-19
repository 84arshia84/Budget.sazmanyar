using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.RequestType;
using vazaef.sazmanyar.Domain.Modles.RequestType;

namespace vazaef.sazmanyar.Application.Services
{
    public class RequestTypeService : IRequestTypeService
    {
        private readonly IRequestTypeRepository _repository;

        public RequestTypeService(IRequestTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetAllRequestTypeDto>> GetAllAsync()
        {
            var types = await _repository.GetAllAsync();
            return types.Select(t => new GetAllRequestTypeDto
            {
                Id = t.Id,
                Description = t.Description
            });
        }

        public async Task<GetByIdRequestTypeDto> GetByIdAsync(long id)
        {
            var type = await _repository.GetByIdAsync(id);
            if (type == null)
                return null;

            return new GetByIdRequestTypeDto
            {
                Id = type.Id,
                Description = type.Description
            };
        }

        public async Task AddAsync(AddRequestTypeDto dto)
        {
            var entity = new RequestType
            {
                Description = dto.Description
            };
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateRequestTypeDto dto)
        {
            var entity = new RequestType
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
