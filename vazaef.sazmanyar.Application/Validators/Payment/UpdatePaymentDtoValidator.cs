using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.Payment;
using vazaef.sazmanyar.Domain.Modles.Allocation;
using vazaef.sazmanyar.Domain.Modles.Payment;

namespace vazaef.sazmanyar.Application.Validators.Payment
{
    public class UpdatePaymentDtoValidator
    {
        private readonly IAllocationRepository _allocationRepo;
        private readonly IPaymentRepository _paymentRepo;

        public UpdatePaymentDtoValidator(
            IAllocationRepository allocationRepo,
            IPaymentRepository paymentRepo)
        {
            _allocationRepo = allocationRepo;
            _paymentRepo = paymentRepo;
        }

        public async Task ValidateAsync(UpdatePaymentDto dto, long paymentId)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.PaymentAmount <= 0)
                throw new ArgumentException("مبلغ پرداخت باید بزرگ‌تر از صفر باشد.");

            // 1. بررسی وجود تخصیص
            var allocation = await _allocationRepo.GetByIdAsync(dto.AllocationId);
            if (allocation == null)
                throw new ArgumentException($"تخصیص با شناسهٔ {dto.AllocationId} یافت نشد.");

            // 2. مجموع کل تخصیص
            var totalAllocated = allocation.AllocationActionBudgetRequests.Sum(x => x.AllocatedAmount);

            // 3. مجموع تمام پرداخت‌های دیگر + کم کردن پرداخت فعلی
            var currentPayment = await _paymentRepo.GetByIdAsync(paymentId);
            if (currentPayment == null)
                throw new KeyNotFoundException("پرداخت مورد نظر یافت نشد.");

            var totalPaidOther = await _paymentRepo.GetTotalPaidByAllocationAsync(dto.AllocationId);
            var totalPaidAfterUpdate = totalPaidOther - currentPayment.PaymentAmount + dto.PaymentAmount;

            var remaining = totalAllocated - totalPaidAfterUpdate;

            if (remaining < 0)
                throw new ArgumentException(
                    $"مبلغ پرداخت ({dto.PaymentAmount:#,0})  باعث می‌شود مجموع پرداخت‌ها که برابر با : ({totalPaidAfterUpdate:#,0})  است از تخصیص ({totalAllocated:#,0}) بیشتر شود. مانده: {(-remaining):#,0}");
        }
    }
}