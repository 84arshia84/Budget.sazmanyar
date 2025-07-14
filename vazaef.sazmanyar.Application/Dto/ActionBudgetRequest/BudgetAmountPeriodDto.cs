using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Application.Dto.ActionBudgetRequest
{
    public class BudgetAmountPeriodDto
    {
        public string EstimationRange { get; set; }
        public string RequestedAmount { get; set; }
        public string PlannedAmount { get; set; }
    }
}
