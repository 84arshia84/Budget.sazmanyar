using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
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

        public async Task<IEnumerable<RequestType>> GetAllRequestTypesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<RequestType> GetRequestTypeByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(RequestType requestType)
        {
            await _repository.AddAsync(requestType);
        }

        public async Task UpdateAsync(RequestType requestType)
        {
            await _repository.UpdateAsync(requestType);
        }

        public async Task DeleteRequestTypeAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }


    }
}
