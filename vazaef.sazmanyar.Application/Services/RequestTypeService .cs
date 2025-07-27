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
    // سرویس مربوط به عملیات روی نوع درخواست (RequestType)
    public class RequestTypeService : IRequestTypeService
    {
        private readonly IRequestTypeRepository _repository;

        // سازنده که ریپازیتوری را تزریق می‌کند
        public RequestTypeService(IRequestTypeRepository repository)
        {
            _repository = repository;
        }

        // دریافت همه نوع‌های درخواست
        public async Task<IEnumerable<GetAllRequestTypeDto>> GetAllAsync()
        {
            var types = await _repository.GetAllAsync();

            // تبدیل موجودیت‌ها به DTO برای ارسال به بیرون
            return types.Select(t => new GetAllRequestTypeDto
            {
                Id = t.Id,
                Description = t.Description
            });
        }

        // دریافت نوع درخواست بر اساس شناسه
        public async Task<GetByIdRequestTypeDto> GetByIdAsync(long id)
        {
            var type = await _repository.GetByIdAsync(id);

            // اگر پیدا نشد مقدار null بازگردانده می‌شود
            if (type == null)
                return null;

            // تبدیل موجودیت به DTO
            return new GetByIdRequestTypeDto
            {
                Id = type.Id,
                Description = type.Description
            };
        }

        // افزودن نوع درخواست جدید
        public async Task AddAsync(AddRequestTypeDto dto)
        {
            var entity = new RequestType
            {
                Description = dto.Description
            };

            // ذخیره در دیتابیس از طریق ریپازیتوری
            await _repository.AddAsync(entity);
        }

        // به‌روزرسانی نوع درخواست بر اساس شناسه
        public async Task UpdateAsync(long id, UpdateRequestTypeDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);

            // اگر نوع درخواست وجود نداشت، خطا پرتاب می‌شود
            if (existing == null)
                throw new KeyNotFoundException($"RequestType {id} not found.");

            // ساخت مدل جدید برای به‌روزرسانی
            var update = new RequestType()
            {
                Id = id,
                Description = dto.Description
            };

            // اعمال تغییرات در دیتابیس
            await _repository.UpdateAsync(update);
        }

        // حذف نوع درخواست بر اساس شناسه
        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
