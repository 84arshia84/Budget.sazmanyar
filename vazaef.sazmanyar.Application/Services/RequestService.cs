using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.ActionBudgetRequest;
using vazaef.sazmanyar.Application.Dto.Request;
using vazaef.sazmanyar.Application.Validators.ActionBudgetRequest;
using vazaef.sazmanyar.Domain.Modles.ActionBudgetRequest;
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
            var actionValidator = new ActionBudgetRequestDtoValidator();

            foreach (var actionDto in dto.ActionBudgetRequests)
                actionValidator.Validate(actionDto);

            var request = new RequestEntity
            {
                RequestTitle = dto.RequestTitle,
                RequestingDepartmentId = dto.RequestingDepartmentId,
                RequestTypeId = dto.RequestTypeId,
                FundingSourceId = dto.FundingSourceId,
                year = dto.year,
                ServiceDescription = dto.ServiceDescription,
                budgetEstimationRanges = dto.budgetEstimationRanges,
            };

            foreach (var actionDto in dto.ActionBudgetRequests)
            {
               
                var updatedPeriods = actionDto.BudgetAmountPeriod.Select(p => new BudgetAmountPeriodDto
                {
                    EstimationRange = $"{dto.year}{p.EstimationRange.PadLeft(2, '0')}", 
                    RequestedAmount = p.RequestedAmount,
                    PlannedAmount = p.PlannedAmount
                }).ToList();

                var actionEntity = new ActionBudgetRequestEntity
                {
                    Title = actionDto.Title,
                    BudgetAmountPeriod = JsonSerializer.Serialize(updatedPeriods), 
                    BudgetRequest = request
                };

                request.ActionBudgetRequests.Add(actionEntity);
            }

            await _repository.AddAsync(request);
        }

        public async Task<GetRequestByIdDto> GetByIdAsync(long id)
        {
            var r = await _repository.GetByIdAsync(id);
            if (r == null)
                return null;

            return new GetRequestByIdDto
            {
                Id = r.Id,
                RequestingDepartmentId = r.RequestingDepartmentId,
                RequestTypeId = r.RequestTypeId,
                FundingSourceId = r.FundingSourceId,
               
                ServiceDescription = r.ServiceDescription,
                year = r.year,
                budgetEstimationRanges = r.budgetEstimationRanges,
                
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
            r.ServiceDescription = dto.ServiceDescription;

            // سال جدید
            r.year = dto.year;

            // به‌روزرسانی EstimationRange در ActionBudgetRequests
            foreach (var ab in r.ActionBudgetRequests)
            {
                var periods = JsonSerializer.Deserialize<List<BudgetAmountPeriodDto>>(ab.BudgetAmountPeriod);

                foreach (var period in periods)
                {
                    // استخراج ماه قبلی از EstimationRange (دو رقم آخر)
                    string oldMonth = period.EstimationRange?.Substring(period.EstimationRange.Length - 2) ?? "01";
                    string month = oldMonth.PadLeft(2, '0');

                    // مقدار جدید estimationRange
                    period.EstimationRange = $"{dto.year}{month}";
                }

                ab.BudgetAmountPeriod = JsonSerializer.Serialize(periods);
            }

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
        public async Task<IEnumerable<GetAllRequestDto>> GetAllWithTotalBudgetAsync()
        {
            var json = await _repository.GetAllRequestsWithTotalBudgetJsonAsync();

            var result = JsonSerializer.Deserialize<IEnumerable<GetAllRequestDto>>(json);

            return result ?? new List<GetAllRequestDto>();
        }
        public async Task<IEnumerable<GetRequestByIdsDto>> GetByIdsAsync(IEnumerable<long> ids)
        {
            var jsonString = await _repository.GetRequestsByIdsWithTotalBudgetJsonAsync(ids);

            var result = JsonSerializer.Deserialize<IEnumerable<GetRequestByIdsDto>>(jsonString);

            return result ?? new List<GetRequestByIdsDto>();
        }


        public async Task<List<RequestDto>> GetAllWithBudgetEstimationAsync()
        {
            var list = await _repository.GetAllWithActionBudgetRequestsAsync();

            return list.Select(r => new RequestDto
            {
                Id = r.Id,
                RequestTitle = r.RequestTitle,
                RequestingDepartmentId = r.RequestingDepartmentId,
                RequestTypeId = r.RequestTypeId,
                FundingSourceId = r.FundingSourceId,
                ApplicationYear = r.year,
                ServiceDescription = r.ServiceDescription,
                BudgetEstimationRanges = r.budgetEstimationRanges, // این مورد در پایین بررسی می‌شود
                ActionBudgetRequests = r.ActionBudgetRequests.Select(ab =>
                {
                    var periods = JsonSerializer.Deserialize<List<BudgetAmountPeriodDto>>(ab.BudgetAmountPeriod);

                    return new ActionBudgetRequestDto
                    {
                        Title = ab.Title,
                        BudgetAmountPeriod = periods.Select(p =>
                        {
                            return new BudgetAmountPeriodDto
                            {
                                EstimationRange = p.EstimationRange, // ✅ بدون تغییر
                                RequestedAmount = p.RequestedAmount,
                                PlannedAmount = p.PlannedAmount
                            };
                        }).ToList()
                    };
                }).ToList()
            }).ToList();
        }

    }
}



