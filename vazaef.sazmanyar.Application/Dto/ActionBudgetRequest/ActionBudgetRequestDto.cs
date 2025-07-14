using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Application.Dto.ActionBudgetRequest
{
    public class ActionBudgetRequestDto
    {
        public string Title { get; set; }

        public List<BudgetAmountPeriodDto> BudgetAmountPeriod { get; set; } = new();

        public decimal TotalActionBudget { get; set; }  // این فقط در DTO هست (نه در Entity)
    }   
}
