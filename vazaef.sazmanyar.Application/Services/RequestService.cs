using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json; // برای سریال‌سازی/دیسریال‌سازی JSON
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.ActionBudgetRequest;
using vazaef.sazmanyar.Application.Dto.Request;
using vazaef.sazmanyar.Application.Validators.ActionBudgetRequest;
using vazaef.sazmanyar.Domain.Modles.ActionBudgetRequest;
using vazaef.sazmanyar.Domain.Modles.Request;

namespace vazaef.sazmanyar.Application.Services
{
    // کلاس Service برای مدیریت عملیات مربوط به درخواست‌ها
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _repository;

        public RequestService(IRequestRepository repository)
        {
            _repository = repository;
        }
        //CreateRequestDto
        // متد افزودن یک درخواست جدید
        public async Task AddAsync(CreateRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var request = new RequestEntity
            {
                RequestTitle = dto.RequestTitle,
                RequestingDepartmentId = dto.RequestingDepartmentId,
                RequestTypeId = dto.RequestTypeId,
                FundingSourceId = dto.FundingSourceId,
                year = dto.year,
                ServiceDescription = dto.ServiceDescription,
                budgetEstimationRanges = dto.budgetEstimationRanges
            };

            // اعتبارسنجی ActionBudgetRequests
            var actionValidator = new ActionBudgetRequestDtoValidator();
            var periodValidator = new BudgetAmountPeriodDtoValidator();

            foreach (var actionDto in dto.ActionBudgetRequests)
            {
                actionValidator.Validate(actionDto);

                foreach (var periodDto in actionDto.BudgetAmountPeriod)
                {
                    periodValidator.Validate(periodDto); // ✅ شامل چک 6 رقمی EstimationRange
                }

                var periods = actionDto.BudgetAmountPeriod.Select(p => new BudgetAmountPeriodDto
                {
                    EstimationRange = p.EstimationRange, // ✅ مستقیم کپی می‌شود (مثلاً 140403)
                    RequestedAmount = p.RequestedAmount,
                    PlannedAmount = p.PlannedAmount
                }).ToList();

                var actionEntity = new ActionBudgetRequestEntity
                {
                    Title = actionDto.Title,
                    BudgetAmountPeriod = JsonSerializer.Serialize(periods),
                    BudgetRequest = request
                };

                request.ActionBudgetRequests.Add(actionEntity);
            }

            await _repository.AddAsync(request);
        }



        // به‌روزرسانی اطلاعات درخواست
        public async Task<bool> UpdateAsync(long id, EditRequestDto dto)
        {
            var r = await _repository.GetByIdAsync(id);
            if (r == null)
                return false;

            // اعتبارسنجی ورودی
            var actionValidator = new ActionBudgetRequestDtoValidator();
            var periodValidator = new BudgetAmountPeriodDtoValidator();

            foreach (var actionDto in dto.ActionBudgetRequests)
            {
                actionValidator.Validate(actionDto);

                foreach (var periodDto in actionDto.BudgetAmountPeriod)
                {
                    periodValidator.Validate(periodDto); // ✅ چک می‌کند 6 رقم و ماه/سال معتبر باشد
                }
            }

            // آپدیت فیلدهای اصلی
            r.RequestTitle = dto.RequestTitle;
            r.RequestingDepartmentId = dto.RequestingDepartmentId;
            r.RequestTypeId = dto.RequestTypeId;
            r.FundingSourceId = dto.FundingSourceId;
            r.year = dto.year;
            r.ServiceDescription = dto.ServiceDescription;
            r.budgetEstimationRanges = dto.budgetEstimationRanges;

            // پاک کردن و بازسازی ActionBudgetRequests
            r.ActionBudgetRequests.Clear();

            foreach (var dtoAb in dto.ActionBudgetRequests)
            {
                var periods = new List<BudgetAmountPeriodDto>();

                foreach (var dtoPeriod in dtoAb.BudgetAmountPeriod)
                {
                    periods.Add(new BudgetAmountPeriodDto
                    {
                        EstimationRange = dtoPeriod.EstimationRange, // ✅ بدون تغییر — مثلاً 140403
                        RequestedAmount = dtoPeriod.RequestedAmount,
                        PlannedAmount = dtoPeriod.PlannedAmount
                    });
                }

                var ab = new ActionBudgetRequestEntity
                {
                    Title = dtoAb.Title,
                    BudgetAmountPeriod = JsonSerializer.Serialize(periods),
                    BudgetRequestId = r.Id
                };

                r.ActionBudgetRequests.Add(ab);
            }

            await _repository.UpdateAsync(r);
            return true;
        }

        // گرفتن یک درخواست بر اساس ID
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



        // حذف یک درخواست
        public async Task<bool> DeleteAsync(long id)
        {
            var r = await _repository.GetByIdAsync(id);
            if (r == null)
                return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        // دریافت تمام درخواست‌ها به همراه جمع بودجه
        public async Task<IEnumerable<GetAllRequestDto>> GetAllWithTotalBudgetAsync()
        {
            var json = await _repository.GetAllRequestsWithTotalBudgetJsonAsync();

            // تبدیل رشته JSON به لیست DTO
            var result = JsonSerializer.Deserialize<IEnumerable<GetAllRequestDto>>(json);

            return result ?? new List<GetAllRequestDto>();
        }

        // دریافت مجموعه‌ای از درخواست‌ها بر اساس ID
        public async Task<IEnumerable<GetRequestByIdsDto>> GetByIdsAsync(IEnumerable<long> ids)
        {
            var jsonString = await _repository.GetRequestsByIdsWithTotalBudgetJsonAsync(ids);

            var result = JsonSerializer.Deserialize<IEnumerable<GetRequestByIdsDto>>(jsonString);

            return result ?? new List<GetRequestByIdsDto>();
        }

        // دریافت تمام درخواست‌ها به همراه ActionBudgetRequestها
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
                BudgetEstimationRanges = r.budgetEstimationRanges,

                // تبدیل هر ActionBudgetRequest به DTO
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
                                EstimationRange = p.EstimationRange,
                                RequestedAmount = p.RequestedAmount,
                                PlannedAmount = p.PlannedAmount
                            };
                        }).ToList()
                    };
                }).ToList()

            }).ToList(); // اجرای نهایی LINQ با ToList()
        }
    }
}

//عبارت توضیح
//JsonSerializer.Serialize(obj)	تبدیل شیء به رشته JSON برای ذخیره در دیتابیس
//JsonSerializer.Deserialize<T>(jsonString)	بازگردانی داده JSON به یک لیست یا شیء قابل استفاده
//Select(...)	عملیات LINQ برای map کردن لیست‌ها (تبدیل آیتم‌ها)
//PadLeft(2, '0')	اطمینان از دو رقمی بودن رشته؛ مثلاً "1" می‌شود "01"
//    ?.Substring(...) ?? "01"	اگر مقدار null باشد، مقدار پیش‌فرض برمی‌گرداند

