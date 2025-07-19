using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.ActionBudgetRequest;

namespace vazaef.sazmanyar.Application.Dto.Request
{
    public class GetRequestByIdDto
    {
        public long Id { get; set; }
        public long RequestingDepartmentId { get; set; }
        public long FundingSourceId { get; set; }
        public long RequestTypeId { get; set; }
        public int year { get; set; }
        public string budgetEstimationRanges { get; set; }
        public string ServiceDescription { get; set; }
       
    }
}
