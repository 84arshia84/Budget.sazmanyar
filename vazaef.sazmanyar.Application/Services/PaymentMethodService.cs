using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.PaymentMethod;
using vazaef.sazmanyar.Domain.Modles.PaymentMethod;

namespace vazaef.sazmanyar.Application.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _repository;

        public PaymentMethodService(IPaymentMethodRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PaymentMethodDto>> GetAllAsync()
        {
            var methods = await _repository.GetAllAsync();
            return methods.Select(m => new PaymentMethodDto { Id = m.Id, Name = m.Name }).ToList();
        }

        public async Task<PaymentMethodDto> GetByIdAsync(long id)
        {
            var method = await _repository.GetByIdAsync(id);
            return new PaymentMethodDto { Id = method.Id, Name = method.Name };
        }

        public async Task CreateAsync(CreatePaymentMethodDto dto)
        {
            var allMethods = await _repository.GetAllAsync();

            if (allMethods.Any(m => m.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
                throw new Exception("روش پرداخت با این نام قبلاً ثبت شده است.");

            var method = new PaymentMethod { Name = dto.Name.Trim() };
            await _repository.AddAsync(method);
        }

        public async Task UpdateAsync(long id, CreatePaymentMethodDto dto)
        {
            // گرفتن رکورد فعلی بر اساس ID
            var method = await _repository.GetByIdAsync(id);
            if (method == null)
                throw new Exception("روش پرداخت یافت نشد.");

            // گرفتن تمام رکوردها برای بررسی تکراری بودن نام جدید
            var allMethods = await _repository.GetAllAsync();

            // اگر رکورد دیگه‌ای با همین نام وجود داشت (که IDش با این فرق داره)
            if (allMethods.Any(m =>
                    m.Id != id &&
                    m.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                throw new Exception("نام وارد شده قبلاً برای روش پرداخت دیگری ثبت شده است.");
            }

            // ویرایش و ذخیره
            method.Name = dto.Name.Trim();
            await _repository.UpdateAsync(method);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
