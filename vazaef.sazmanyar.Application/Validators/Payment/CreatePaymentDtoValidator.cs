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
    public class CreatePaymentDtoValidator
    {
        private readonly IAllocationRepository _allocationRepo;
        private readonly IPaymentRepository _paymentRepo;

        public CreatePaymentDtoValidator(
            IAllocationRepository allocationRepo,
            IPaymentRepository paymentRepo)
        {
            _allocationRepo = allocationRepo;
            _paymentRepo = paymentRepo;
        }

        public async Task ValidateAsync(CreatePaymentDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.PaymentAmount <= 0)
                throw new ArgumentException("مبلغ پرداخت باید بزرگ‌تر از صفر باشد.");

            // 1. بررسی وجود تخصیص
            var allocation = await _allocationRepo.GetByIdAsync(dto.AllocationId);
            if (allocation == null)
                throw new ArgumentException($"تخصیص با شناسهٔ {dto.AllocationId} یافت نشد.");

            // 2. مجموع کل مبالغ تخصیص‌ یافته
            var totalAllocated = allocation
                .AllocationActionBudgetRequests
                .Sum(x => x.AllocatedAmount);

            // 3. مجموع مبالغ پرداخت‌شده تا کنون
            var totalPaid = await _paymentRepo
                .GetTotalPaidByAllocationAsync(dto.AllocationId);

            var remaining = totalAllocated - totalPaid;
            if (dto.PaymentAmount > remaining)
                throw new ArgumentException(
                    $"مبلغ پرداخت ({dto.PaymentAmount:#,0}) نمی‌تواند بیشتر از ماندهٔ بودجه ({remaining:#,0}) باشد.");
        }
    }
}
