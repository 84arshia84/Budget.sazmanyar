using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.Payment;
using vazaef.sazmanyar.Application.Validators.Payment;
using vazaef.sazmanyar.Domain.Modles.Payment;

namespace vazaef.sazmanyar.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;
        private readonly CreatePaymentDtoValidator _validator;

        public PaymentService(
            IPaymentRepository repository,
            CreatePaymentDtoValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task CreateAsync(CreatePaymentDto dto)
        {
            // اعتبارسنجی اختصاصی
            await _validator.ValidateAsync(dto);

            var payment = new Payment
            {
                PaymentDate = dto.PaymentDate,
                PaymentAmount = dto.PaymentAmount,
                AllocationId = dto.AllocationId,
                PaymentMethodId = dto.PaymentMethodId
            };

            await _repository.AddAsync(payment);
        }

        public async Task UpdateAsync(UpdatePaymentDto dto)
        {
            var payment = await _repository.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException("Payment not found");

            payment.PaymentDate = dto.PaymentDate;
            payment.PaymentAmount = dto.PaymentAmount;
            payment.AllocationId = dto.AllocationId;
            payment.PaymentMethodId = dto.PaymentMethodId;

            await _repository.UpdateAsync(payment);
        }

        public Task DeleteAsync(long id)
            => _repository.DeleteAsync(id);

        public async Task<PaymentDto?> GetByIdAsync(long id)
        {
            var p = await _repository.GetByIdAsync(id);
            if (p == null) return null;

            return new PaymentDto
            {
                Id = p.Id,
                PaymentDate = p.PaymentDate,
                PaymentAmount = p.PaymentAmount,
                AllocationId = p.AllocationId,
                PaymentMethodId = p.PaymentMethodId
            };
        }

        public async Task<List<PaymentDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
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