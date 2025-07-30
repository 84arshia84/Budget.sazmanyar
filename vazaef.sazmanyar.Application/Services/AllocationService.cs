using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.Allocation;
using vazaef.sazmanyar.Application.Validators.Allocation;
using vazaef.sazmanyar.Domain.Modles.Allocation;
using vazaef.sazmanyar.Domain.Modles.AllocationActionBudgetRequest;

namespace vazaef.sazmanyar.Application.Services
{
    public class AllocationService : IAllocationService
    {
        private readonly IAllocationRepository _repo;
        private readonly UpdateAllocationDtoValidator _validator;

        public AllocationService(
            IAllocationRepository repo,
            UpdateAllocationDtoValidator validator)
        {
            _repo = repo;
            _validator = validator;
        }

        public async Task AddAsync(CreateAllocationDto dto)
        {
            var allocation = new Allocation
            {
                Title = dto.AllocationTitle,
                Date = dto.AllocationDate,
                RequestId = dto.BudgetRequestId,
                AllocationActionBudgetRequests = dto.ActionAllocations.Select(a => new AllocationActionBudgetRequest
                {
                    ActionBudgetRequestId = a.ActionBudgetRequestId,
                    AllocatedAmount = a.AllocatedAmount
                    // AllocationId خودش پر می‌شه وقتی ذخیره شه
                }).ToList()
            };

            await _repo.AddAsync(allocation);
        }

        public async Task UpdateAsync(long id, UpdateAllocationDto dto)
        {
            // ✅ ولیدیشن اولیه
            _validator.Validate(dto);

            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Allocation {id} not found.");

            // ✅ آپدیت فیلدهای اصلی
            existing.Title = dto.AllocationTitle;
            existing.Date = dto.AllocationDate;
            existing.RequestId = dto.BudgetRequestId;

            // ✅ پاک کردن و اضافه مجدد (بدون مشکل temporary value)
            existing.AllocationActionBudgetRequests.Clear();

            foreach (var item in dto.ActionAllocations)
            {
                existing.AllocationActionBudgetRequests.Add(new AllocationActionBudgetRequest
                {
                    ActionBudgetRequestId = item.ActionBudgetRequestId,
                    AllocatedAmount = item.AllocatedAmount
                    // ❌ AllocationId نیاز نیست — EF خودش پر می‌کنه
                });
            }

            await _repo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(long id)
            => await _repo.DeleteAsync(id);

        public async Task<AllocationDto?> GetByIdAsync(long id)
        {
            var allocation = await _repo.GetByIdAsync(id);
            if (allocation == null) return null;

            return new AllocationDto
            {
                Id = allocation.Id,
                Title = allocation.Title,
                Date = allocation.Date,
                RequestId = allocation.RequestId,
                ActionAllocations = allocation.AllocationActionBudgetRequests.Select(a => new ActionAllocationDto
                {
                    ActionBudgetRequestId = a.ActionBudgetRequestId,
                    AllocatedAmount = a.AllocatedAmount
                }).ToList()
            };
        }

        public async Task<List<AllocationDto>> GetAllAsync()
        {
            var allocations = await _repo.GetAllAsync();
            return allocations.Select(a => new AllocationDto
            {
                Id = a.Id,
                Title = a.Title,
                Date = a.Date,
                RequestId = a.RequestId,
                ActionAllocations = a.AllocationActionBudgetRequests.Select(abr => new ActionAllocationDto
                {
                    ActionBudgetRequestId = abr.ActionBudgetRequestId,
                    AllocatedAmount = abr.AllocatedAmount
                }).ToList()
            }).ToList();
        }
    }
}