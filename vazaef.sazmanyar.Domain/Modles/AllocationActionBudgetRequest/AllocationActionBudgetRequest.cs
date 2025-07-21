using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllocationModel = vazaef.sazmanyar.Domain.Modles.Allocation.Allocation;
namespace vazaef.sazmanyar.Domain.Modles.AllocationActionBudgetRequest
{
    public class AllocationActionBudgetRequest
    {
        public long AllocationId { get; set; }
        public AllocationModel Allocation { get; set; }
        public long ActionBudgetRequestId { get; set; }
        public ActionBudgetRequest.ActionBudgetRequestEntity ActionBudgetRequest { get; set; }

        public decimal AllocatedAmount { get; set; }
    }

}
