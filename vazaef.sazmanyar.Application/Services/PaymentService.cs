using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.Payment;
using vazaef.sazmanyar.Domain.Modles.Payment;

namespace vazaef.sazmanyar.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;

        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreatePaymentDto dto)
        {
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
            var payment = await _repository.GetByIdAsync(dto.Id);
            if (payment == null) throw new Exception("Payment not found");

            payment.PaymentDate = dto.PaymentDate;
            payment.PaymentAmount = dto.PaymentAmount;
            payment.AllocationId = dto.AllocationId;
            payment.PaymentMethodId = dto.PaymentMethodId;

            await _repository.UpdateAsync(payment);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<PaymentDto> GetByIdAsync(long id)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null) return null;

            return new PaymentDto
            {
                Id = payment.Id,
                PaymentDate = payment.PaymentDate,
                PaymentAmount = payment.PaymentAmount,
                AllocationId = payment.AllocationId,
                PaymentMethodId = payment.PaymentMethodId
            };
        }

        public async Task<List<PaymentDto>> GetAllAsync()
        {
            var payments = await _repository.GetAllAsync();
            return payments.Select(p => new PaymentDto
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