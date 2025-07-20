using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Application.Dto.Request
{
    public class GetRequestByIdsDto
    {
        public long Id { get; set; }
        public string RequestTitle { get; set; }
        public int RequestingDepartmentId { get; set; }
        public int RequestTypeId { get; set; }
        public int FundingSourceId { get; set; }
        public int Year { get; set; }
        public string ServiceDescription { get; set; }
        public string BudgetEstimationRanges { get; set; }
        public decimal TotalActionBudget { get; set; }
    }
}
