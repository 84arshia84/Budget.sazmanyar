using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.Request;
using vazaef.sazmanyar.Domain.Modles.Request;

namespace vazaef.sazmanyar.Application.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _repository;

        public RequestService(IRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(CreateRequestDto dto)
        {
            var request = new Request
            {
                RequestTitle = dto.RequestTitle,
                RequestingDepartmentId = dto.RequestingDepartmentId,
                RequestTypeId = dto.RequestTypeId,
                FundingSourceId = dto.FundingSourceId,
                ApplicationYear = dto.ApplicationYear,
                TimeFrame = dto.TimeFrame,
                ServiceDescription = dto.ServiceDescription
            };

            await _repository.AddAsync(request);
        }

        public async Task<IEnumerable<RequestDto>> GetAllAsync()
        {
            var requests = await _repository.GetAllAsync();
            return requests.Select(r => new RequestDto
            {
                Id = r.Id,
                RequestTitle = r.RequestTitle,
                RequestingDepartmentId = r.RequestingDepartmentId,
                RequestTypeId = r.RequestTypeId,
                FundingSourceId = r.FundingSourceId,
                ApplicationYear = r.ApplicationYear,
                TimeFrame = r.TimeFrame,
                ServiceDescription = r.ServiceDescription,
            });
        }

        public async Task<RequestDto> GetByIdAsync(long id)
        {
            var r = await _repository.GetByIdAsync(id);
            if (r == null)
                return null;

            return new RequestDto
            {
                Id = r.Id,
                RequestTitle = r.RequestTitle,
                RequestingDepartmentId = r.RequestingDepartmentId,
                RequestTypeId = r.RequestTypeId,
                FundingSourceId = r.FundingSourceId,
                ApplicationYear = r.ApplicationYear,
                TimeFrame = r.TimeFrame,
                ServiceDescription = r.ServiceDescription,
            };
        }

        public async Task<bool> UpdateAsync(long id, EditRequestDto dto)
        {
            var r = await _repository.GetByIdAsync(id);
            if (r == null)
                return false;

            r.RequestTitle = dto.RequestTitle;
            r.RequestingDepartmentId = dto.RequestingDepartmentId;
            r.RequestTypeId = dto.RequestTypeId;
            r.FundingSourceId = dto.FundingSourceId;
            r.ApplicationYear = dto.ApplicationYear;
            r.TimeFrame = dto.TimeFrame;
            r.ServiceDescription = dto.ServiceDescription;

            await _repository.UpdateAsync(r);
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var r = await _repository.GetByIdAsync(id);
            if (r == null)
                return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}

