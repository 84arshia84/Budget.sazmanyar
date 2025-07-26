using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.ActionBudgetRequest;

namespace vazaef.sazmanyar.Application.Dto.Request
{
    public class RequestWithActionBudgetDto
    {
        public string RequestTitle { get; set; }
        public long RequestingDepartmentId { get; set; }
        public long RequestTypeId { get; set; }
        public long FundingSourceId { get; set; }
        public int Year { get; set; }
        public string ServiceDescription { get; set; }
        public string BudgetEstimationRanges { get; set; }

        public List<ActionBudgetRequestDto> ActionBudgetRequests { get; set; }
    }

}
