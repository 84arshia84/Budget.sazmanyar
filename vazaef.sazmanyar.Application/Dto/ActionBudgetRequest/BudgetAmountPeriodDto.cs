using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Application.Dto.ActionBudgetRequest
{
    public class BudgetAmountPeriodDto
    {
        [Required]
        public string EstimationRange { get; set; }
        public string RequestedAmount { get; set; }
        public string PlannedAmount { get; set; }
    }
}
