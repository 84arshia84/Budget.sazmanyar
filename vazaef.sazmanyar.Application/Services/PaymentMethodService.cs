using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.PaymentMethod;
using vazaef.sazmanyar.Domain.Modles.PaymentMethod;

namespace vazaef.sazmanyar.Application.Services
{
    // این کلاس مسئول پیاده‌سازی عملیات مربوط به روش‌های پرداخت (Payment Methods) است
    public class PaymentMethodService : IPaymentMethodService
    {
        // تعریف Repository به صورت readonly و تزریق آن از طریق سازنده
        private readonly IPaymentMethodRepository _repository;

        public PaymentMethodService(IPaymentMethodRepository repository)
        {
            _repository = repository;
        }

        // متدی برای دریافت تمام روش‌های پرداخت و تبدیل آنها به DTO
        public async Task<List<PaymentMethodDto>> GetAllAsync()
        {
            // گرفتن لیست از مدل‌های دامنه‌ای از دیتابیس
            var methods = await _repository.GetAllAsync();

            // استفاده از LINQ برای تبدیل هر رکورد Domain به DTO
            // متد Select برای map کردن مقادیر استفاده می‌شود (مثل foreach با خروجی)
            // متد ToList() نتیجه را از IEnumerable به List تبدیل می‌کند (برای راحتی در UI یا ارسال خروجی)
            return methods.Select(m => new PaymentMethodDto
            {
                Id = m.Id,
                Name = m.Name
            }).ToList();
        }

        // دریافت یک روش پرداخت خاص با استفاده از شناسه (ID)
        public async Task<PaymentMethodDto> GetByIdAsync(long id)
        {
            var method = await _repository.GetByIdAsync(id);
            // تبدیل مدل دامنه به DTO
            return new PaymentMethodDto
            {
                Id = method.Id,
                Name = method.Name
            };
        }

        // ایجاد یک روش پرداخت جدید
        public async Task CreateAsync(CreatePaymentMethodDto dto)
        {
            // گرفتن همه روش‌های پرداخت برای بررسی تکراری بودن نام
            var allMethods = await _repository.GetAllAsync();

            // بررسی اینکه آیا نام وارد شده قبلاً ثبت شده است یا نه
            // متد Any بررسی می‌کند آیا حداقل یک آیتم با شرط مورد نظر وجود دارد
            if (allMethods.Any(m => m.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
                throw new Exception("روش پرداخت با این نام قبلاً ثبت شده است.");

            // ساخت یک آبجکت جدید از مدل دامنه
            var method = new PaymentMethod
            {
                Name = dto.Name.Trim() // حذف فاصله‌های اضافی از ابتدا و انتهای رشته
            };

            // ذخیره در دیتابیس
            await _repository.AddAsync(method);
        }

        // به‌روزرسانی یک روش پرداخت موجود
        public async Task UpdateAsync(long id, CreatePaymentMethodDto dto)
        {
            // مرحله اول: پیدا کردن رکورد اصلی از دیتابیس
            var method = await _repository.GetByIdAsync(id);
            if (method == null)
                throw new Exception("روش پرداخت یافت نشد.");

            // گرفتن لیست کامل برای بررسی تکراری بودن نام جدید
            var allMethods = await _repository.GetAllAsync();

            // بررسی اینکه آیا روش پرداخت دیگری با همان نام وجود دارد یا نه
            if (allMethods.Any(m =>
                    m.Id != id && // خود این رکورد نباشد
                    m.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                throw new Exception("نام وارد شده قبلاً برای روش پرداخت دیگری ثبت شده است.");
            }

            // اعمال تغییرات و ذخیره
            method.Name = dto.Name.Trim();
            await _repository.UpdateAsync(method);
        }

        // حذف یک روش پرداخت بر اساس ID
        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
