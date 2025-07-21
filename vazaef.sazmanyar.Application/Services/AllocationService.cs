using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.Allocation;
using vazaef.sazmanyar.Domain.Modles.Allocation;
using vazaef.sazmanyar.Domain.Modles.AllocationActionBudgetRequest;

namespace vazaef.sazmanyar.Application.Services
{
    public class AllocationService : IAllocationService
    {
        private readonly IAllocationRepository _repo;

        public AllocationService(IAllocationRepository repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(CreateAllocationDto dto)
        {
            var allocation = new Allocation
            {
                Title = dto.AllocationTitle,
                Date = dto.AllocationDate,
                RequestId = dto.BudgetRequestId,
                AllocationActionBudgetRequests = dto.ActionAllocations
                    .Select(a => new AllocationActionBudgetRequest
                    {
                        ActionBudgetRequestId = a.ActionBudgetRequestId,
                        AllocatedAmount = a.AllocatedAmount
                    }).ToList()
            };

            await _repo.AddAsync(allocation);
        }

        public async Task UpdateAsync(long id, UpdateAllocationDto dto)
        {
            // بارگذاری موجود
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Allocation {id} not found.");

            // ساخت یک شی موقت برای انتقال داده‌ها
            var updated = new Allocation
            {
                Id = id,
                Title = dto.AllocationTitle,
                Date = dto.AllocationDate,
                RequestId = dto.BudgetRequestId,
                AllocationActionBudgetRequests = dto.ActionAllocations
                    .Select(x => new AllocationActionBudgetRequest
                    {
                        ActionBudgetRequestId = x.ActionBudgetRequestId,
                        AllocatedAmount = x.AllocatedAmount
                    })
                    .ToList()
            };

            // سرویس به Repository می‌سپارد
            await _repo.UpdateAsync(updated);
        }
        public async Task DeleteAsync(long id) => await _repo.DeleteAsync(id);

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
                ActionAllocations = allocation.AllocationActionBudgetRequests
                    .Select(a => new ActionAllocationDto
                    {
                        ActionBudgetRequestId = a.ActionBudgetRequestId,
                        AllocatedAmount = a.AllocatedAmount
                    }).ToList()
            };
        }

        public async Task<List<AllocationDto>> GetAllAsync()
        {
            var allocations = await _repo.GetAllAsync();
            return allocations.Select(allocation => new AllocationDto
            {
                Id = allocation.Id,
                Title = allocation.Title,
                Date = allocation.Date,
                RequestId = allocation.RequestId,
                ActionAllocations = allocation.AllocationActionBudgetRequests
                    .Select(a => new ActionAllocationDto
                    {
                        ActionBudgetRequestId = a.ActionBudgetRequestId,
                        AllocatedAmount = a.AllocatedAmount
                    }).ToList()
            }).ToList();
        }
    }
}
