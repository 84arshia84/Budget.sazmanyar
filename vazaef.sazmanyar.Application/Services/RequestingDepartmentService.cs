using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.RequestingDepartmen;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;
using vazaef.sazmanyar.Domain.Modles.RequestType;

namespace vazaef.sazmanyar.Application.Services
{
    // این کلاس وظیفه مدیریت منطق تجاری مربوط به "واحدهای درخواست‌دهنده" را دارد
    public class RequestingDepartmenService : IRequestingDepartmenService
    {
        // وابستگی به Repository برای دسترسی به پایگاه داده
        private readonly IRequestingDepartmenRepository _repository;

        // سازنده کلاس با استفاده از Dependency Injection
        public RequestingDepartmenService(IRequestingDepartmenRepository repository)
        {
            _repository = repository;
        }

        // گرفتن لیست تمام واحدهای درخواست‌دهنده
        public async Task<IEnumerable<GetAllRequestingDepartmenDto>> GetAllAsync()
        {
            // دریافت همه رکوردها از پایگاه داده
            var departments = await _repository.GetAllAsync();

            // استفاده از LINQ و Select برای تبدیل Domain Model به DTO
            // در اینجا از ToList استفاده نشده چون خروجی IEnumerable کفایت می‌کند
            return departments.Select(d => new GetAllRequestingDepartmenDto
            {
                Id = d.Id,
                Description = d.Description
            });
        }

        // گرفتن یک واحد بر اساس شناسه (ID)
        public async Task<GetByIdRequestingDepartmenDto> GetByIdAsync(long id)
        {
            // دریافت واحد از دیتابیس
            var department = await _repository.GetByIdAsync(id);

            // بررسی اینکه آیا چنین رکوردی وجود دارد یا نه
            if (department == null)
                return null;

            // تبدیل مدل دامنه به DTO و بازگشت به UI
            return new GetByIdRequestingDepartmenDto
            {
                Id = department.Id,
                Description = department.Description
            };
        }

        // افزودن یک واحد جدید
        public async Task AddAsync(AddRequestingDepartmenDto dto)
        {
            // ساخت شیء جدید از نوع دامنه‌ای با اطلاعات ورودی
            var entity = new RequestingDepartmen
            {
                Description = dto.Description
            };

            // ذخیره در پایگاه داده از طریق Repository
            await _repository.AddAsync(entity);
        }

        // به‌روزرسانی اطلاعات یک واحد بر اساس ID
        public async Task UpdateAsync(long id, UpdateRequestingDepartmenDto dto)
        {
            // ابتدا بررسی وجود رکورد در پایگاه داده
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"UpdateRequestingDepartmenDto {id} not found.");

            // ساخت آبجکت جدید با مقادیر به‌روزشده
            var update = new RequestingDepartmen()
            {
                Id = id,
                Description = dto.Description
            };

            // به‌روزرسانی در پایگاه داده
            await _repository.UpdateAsync(update);
        }

        // حذف یک واحد درخواست‌دهنده
        public async Task DeleteAsync(long id)
        {
            // حذف بر اساس شناسه
            await _repository.DeleteAsync(id);
        }
    }
}
