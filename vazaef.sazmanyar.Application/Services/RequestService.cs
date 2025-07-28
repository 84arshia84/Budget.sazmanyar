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

        // متد افزودن یک درخواست جدید
        public async Task AddAsync(CreateRequestDto dto)
        {
            var actionValidator = new ActionBudgetRequestDtoValidator();

            // اعتبارسنجی تمام ActionBudgetRequestها
            foreach (var actionDto in dto.ActionBudgetRequests)
                actionValidator.Validate(actionDto);

            // ایجاد یک موجودیت Request
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
                // قبل از ساخت JSON، اعتبارسنجی ماه انجام می‌شود
                var updatedPeriods = actionDto.BudgetAmountPeriod.Select(p =>
                {
                    var month = p.EstimationRange.PadLeft(2, '0'); // اطمینان از اینکه مثلا "2" بشه "02"

                    // ✅ اعتبارسنجی اینکه EstimationRange بین 01 تا 12 باشد
                    if (!int.TryParse(month, out int monthInt) || monthInt < 1 || monthInt > 12)
                        throw new ArgumentException($"ماه وارد شده ({month}) معتبر نیست. مقدار باید بین 01 تا 12 باشد.");

                    return new BudgetAmountPeriodDto
                    {
                        EstimationRange = $"{dto.year}{month}", // ترکیب سال و ماه مثلا: 202501
                        RequestedAmount = p.RequestedAmount,
                        PlannedAmount = p.PlannedAmount
                    };
                }).ToList();

                // ساخت ActionBudgetRequestEntity و تبدیل لیست به رشته JSON
                var actionEntity = new ActionBudgetRequestEntity
                {
                    Title = actionDto.Title,
                    BudgetAmountPeriod = JsonSerializer.Serialize(updatedPeriods),
                    BudgetRequest = request
                };

                request.ActionBudgetRequests.Add(actionEntity);
            }

            await _repository.AddAsync(request); // ذخیره در دیتابیس
        }
        // به‌روزرسانی اطلاعات درخواست
        public async Task<bool> UpdateAsync(long id, EditRequestDto dto)
        {
            var r = await _repository.GetByIdAsync(id);
            if (r == null)
                return false;

            // اعتبارسنجی داده‌های ورودی برای ActionBudgetRequests
            var actionValidator = new ActionBudgetRequestDtoValidator();
            var periodValidator = new BudgetAmountPeriodDtoValidator();

            foreach (var actionDto in dto.ActionBudgetRequests)
            {
                actionValidator.Validate(actionDto); // ولیدیت کلی Action

                foreach (var period in actionDto.BudgetAmountPeriod)
                {
                    periodValidator.Validate(period); // ✅ ولیدیت period (requestedAmount, plannedAmount, EstimationRange)
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
                    string month = (dtoPeriod.EstimationRange ?? "01").PadLeft(2, '0').Substring(0, 2);
                    periods.Add(new BudgetAmountPeriodDto
                    {
                        EstimationRange = $"{dto.year}{month}",
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

