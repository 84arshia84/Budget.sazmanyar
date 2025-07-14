using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.ActionBudgetRequest;

namespace vazaef.sazmanyar.Application.Dto.Request
{
    public class CreateRequestDto
    {
        public string RequestTitle { get; set; }
        public long RequestingDepartmentId { get; set; }
        public long RequestTypeId { get; set; }
        public long FundingSourceId { get; set; }
        public int ApplicationYear { get; set; }
        public int TimeFrame { get; set; }
        public string ServiceDescription { get; set; }

        public List<ActionBudgetRequestDto> ActionBudgetRequests { get; set; } = new();
    }
}
