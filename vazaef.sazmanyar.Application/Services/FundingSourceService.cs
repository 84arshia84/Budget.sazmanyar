using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.FundingSource;
using vazaef.sazmanyar.Domain.Modles.fundingSource;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;

namespace vazaef.sazmanyar.Application.Services
{
    // این کلاس مسئول پیاده‌سازی منطق سرویس مربوط به منابع تأمین مالی (Funding Source) است
    // این کلاس به Interface مربوطه (IFundingSourceService) متصل است تا از اصل SOLID پیروی کند
    public class FundingSourceService : IFundingSourceService
    {
        // تعریف Repository به صورت readonly که از طریق سازنده تزریق می‌شود
        private readonly IFundingSourceRepository _fundingSourceRepository;

        // Constructor - پیاده‌سازی Dependency Injection برای Repository
        public FundingSourceService(IFundingSourceRepository fundingSourceRepository)
        {
            _fundingSourceRepository = fundingSourceRepository;
        }

        // متد گرفتن همه منابع تأمین مالی
        public async Task<IEnumerable<GetAllFundingSourceDto>> GetAllAsync()
        {
            // دریافت لیست از Repository (از نوع IEnumerable<FundingSource>)
            var sources = await _fundingSourceRepository.GetAllAsync();

            // استفاده از LINQ و متد Select برای تبدیل هر آیتم Domain Model به DTO
            // Select مثل یک حلقه foreach برای هر آیتم عمل می‌کند و یک آیتم جدید می‌سازد
            return sources.Select(s => new GetAllFundingSourceDto
            {
                Id = s.Id,
                Description = s.Description
            });
            // چون خروجی IEnumerable است، نیازی به ToList نیست
        }

        // گرفتن اطلاعات یک منبع تأمین مالی خاص با استفاده از ID
        public async Task<GetByIdFundingSourceDto> GetByIdAsync(long id)
        {
            // دریافت منبع خاص از Repository
            var source = await _fundingSourceRepository.GetByIdAsync(id);

            // بررسی وجود منبع - اگر نبود مقدار null برمی‌گردد
            if (source == null)
                return null;

            // تبدیل Domain Model به DTO
            return new GetByIdFundingSourceDto
            {
                Id = source.Id,
                Description = source.Description
            };
        }

        // متد افزودن منبع تأمین مالی جدید
        public async Task AddAsync(AddFundingSourceDto dto)
        {
            // ساخت آبجکت Domain Model از روی DTO
            var source = new FundingSource
            {
                Description = dto.Description
            };

            // ذخیره در دیتابیس با استفاده از Repository
            await _fundingSourceRepository.AddAsync(source);
        }

        // متد به‌روزرسانی یک منبع موجود با استفاده از ID و DTO جدید
        public async Task UpdateAsync(long id, UpdateFundingSourceDto dto)
        {
            // مرحله اول: دریافت داده فعلی
            var existing = await _fundingSourceRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"FundingSource {id} not found."); // در صورت نبودن، خطا پرتاب می‌شود

            // به‌روزرسانی مقدار Description
            existing.Description = dto.Description;

            // ارسال آبجکت به Repository برای ذخیره تغییرات
            await _fundingSourceRepository.UpdateAsync(existing);
        }

        // متد حذف یک منبع از دیتابیس
        public async Task DeleteAsync(long id)
        {
            // حذف از طریق Repository
            await _fundingSourceRepository.DeleteAsync(id);
        }
    }
}