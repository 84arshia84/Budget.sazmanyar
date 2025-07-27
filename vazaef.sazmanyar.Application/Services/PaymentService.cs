using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.Payment;
using vazaef.sazmanyar.Application.Validators.Payment;
using vazaef.sazmanyar.Domain.Modles.Payment;

namespace vazaef.sazmanyar.Application.Services
{
    // این کلاس مسئول منطق تجاری مرتبط با پرداخت‌ها (Payments) است
    public class PaymentService : IPaymentService
    {
        // تعریف وابستگی‌ها به صورت readonly
        private readonly IPaymentRepository _repository;
        private readonly CreatePaymentDtoValidator _validator;

        // تزریق وابستگی‌ها از طریق Constructor (Dependency Injection)
        public PaymentService(
            IPaymentRepository repository,
            CreatePaymentDtoValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        // متدی برای ایجاد یک پرداخت جدید
        public async Task CreateAsync(CreatePaymentDto dto)
        {
            // اجرای عملیات اعتبارسنجی سفارشی روی DTO ورودی
            await _validator.ValidateAsync(dto);

            // ساخت آبجکت دامنه برای ذخیره‌سازی در پایگاه داده
            var payment = new Payment
            {
                PaymentDate = dto.PaymentDate,
                PaymentAmount = dto.PaymentAmount,
                AllocationId = dto.AllocationId,
                PaymentMethodId = dto.PaymentMethodId
            };

            // ذخیره در دیتابیس از طریق Repository
            await _repository.AddAsync(payment);
        }

        // متد به‌روزرسانی یک پرداخت موجود
        public async Task UpdateAsync(UpdatePaymentDto dto)
        {
            // دریافت پرداخت از دیتابیس، اگر نبود استثناء پرتاب می‌شود
            // استفاده از ?? برای بررسی null و جایگزینی با throw
            var payment = await _repository.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException("Payment not found");

            // اعمال تغییرات
            payment.PaymentDate = dto.PaymentDate;
            payment.PaymentAmount = dto.PaymentAmount;
            payment.AllocationId = dto.AllocationId;
            payment.PaymentMethodId = dto.PaymentMethodId;

            // بروزرسانی در دیتابیس
            await _repository.UpdateAsync(payment);
        }

        // متد حذف یک پرداخت از روی شناسه
        public Task DeleteAsync(long id)
            => _repository.DeleteAsync(id); // حذف مستقیم بدون عملیات اضافی

        // دریافت اطلاعات یک پرداخت خاص
        public async Task<PaymentDto?> GetByIdAsync(long id)
        {
            // گرفتن اطلاعات از دیتابیس
            var p = await _repository.GetByIdAsync(id);
            if (p == null) return null;

            // تبدیل مدل دامنه به DTO جهت بازگشت به UI
            return new PaymentDto
            {
                Id = p.Id,
                PaymentDate = p.PaymentDate,
                PaymentAmount = p.PaymentAmount,
                AllocationId = p.AllocationId,
                PaymentMethodId = p.PaymentMethodId
            };
        }

        // گرفتن لیست کامل پرداخت‌ها
        public async Task<List<PaymentDto>> GetAllAsync()
        {
            // دریافت لیست از دیتابیس
            var list = await _repository.GetAllAsync();

            // استفاده از LINQ برای تبدیل مدل‌های دامنه به DTO
            // متد Select برای map کردن (تبدیل) هر آبجکت استفاده می‌شود
            // ToList برای تبدیل خروجی IEnumerable به List
            return list.Select(p => new PaymentDto
            {
                Id = p.Id,
                PaymentDate = p.PaymentDate,
                PaymentAmount = p.PaymentAmount,
                AllocationId = p.AllocationId,
                PaymentMethodId = p.PaymentMethodId
            }).ToList();
        }
    }
}
