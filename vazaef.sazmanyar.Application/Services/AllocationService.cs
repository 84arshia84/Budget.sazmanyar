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
                AllocationActionBudgetRequests = dto.ActionAllocations.Select(a => new AllocationActionBudgetRequest
                {
                    ActionBudgetRequestId = a.ActionBudgetRequestId,
                    AllocatedAmount = a.AllocatedAmount
                }).ToList()
            };

            await _repo.AddAsync(allocation);
        }
    }
}
