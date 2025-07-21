using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Application.Dto.Allocation
{
    public class CreateAllocationDto
    {
        public string AllocationTitle { get; set; }
        public DateTime AllocationDate { get; set; }
        public long BudgetRequestId { get; set; }
        public List<ActionAllocationDto> ActionAllocations { get; set; } = new();
    }
    public class ActionAllocationDto
    {
        public long ActionBudgetRequestId { get; set; }
        public decimal AllocatedAmount { get; set; }
    }
}
