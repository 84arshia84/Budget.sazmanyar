using System;
using System.Linq;
using vazaef.sazmanyar.Application.Dto.Allocation;

namespace vazaef.sazmanyar.Application.Validators.Allocation
{
    public class UpdateAllocationDtoValidator
    {
        public void Validate(UpdateAllocationDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.AllocationTitle))
                throw new ArgumentException("عنوان تخصیص نمی‌تواند خالی باشد.");

            if (dto.AllocationDate == default)
                throw new ArgumentException("تاریخ تخصیص نمی‌تواند خالی باشد.");

            if (dto.BudgetRequestId <= 0)
                throw new ArgumentException("شناسه درخواست بودجه نامعتبر است.");

            if (dto.ActionAllocations == null || !dto.ActionAllocations.Any())
                throw new ArgumentException("حداقل یک آیتم تخصیص باید وارد شود.");

            foreach (var item in dto.ActionAllocations)
            {
                if (item.ActionBudgetRequestId <= 0)
                    throw new ArgumentException("شناسه درخواست بودجه عملیاتی نامعتبر است.");

                if (item.AllocatedAmount <= 0)
                    throw new ArgumentException("مبلغ تخصیص باید بزرگ‌تر از صفر باشد.");
            }
        }
    }
}