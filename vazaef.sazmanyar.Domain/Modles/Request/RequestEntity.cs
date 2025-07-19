using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;
using static System.Net.Mime.MediaTypeNames;
using vazaef.sazmanyar.Domain.Modles.Request;
using RequestModel = vazaef.sazmanyar.Domain.Modles.Request.RequestEntity;
using vazaef.sazmanyar.Domain.Modles.ActionBudgetRequest;
namespace vazaef.sazmanyar.Domain.Modles.Request
{
    public class RequestEntity
    {
        public long Id { get; set; }
        public string RequestTitle { get; set; }
        public long RequestingDepartmentId { get; set; }
        public long RequestTypeId { get; set; }
        public long FundingSourceId { get; set; }
        public int year { get; set; }
        public string ServiceDescription { get; set; }
        public string budgetEstimationRanges { get; set; }

        public RequestingUnit.RequestingDepartmen RequestingDepartment { get; set; }
        public RequestType.RequestType RequestType { get; set; }
        public PlaceOfFinancing.FundingSource FundingSource { get; set; }

        public ICollection<ActionBudgetRequestEntity> ActionBudgetRequests { get; set; } = new List<ActionBudgetRequestEntity>();

    }


}



