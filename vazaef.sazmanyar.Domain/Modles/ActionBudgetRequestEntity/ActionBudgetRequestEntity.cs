using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.Request;
using ActionBudgetRequestModel = vazaef.sazmanyar.Domain.Modles.ActionBudgetRequest.ActionBudgetRequestEntity;
using RequestModel = vazaef.sazmanyar.Domain.Modles.Request.RequestEntity;

namespace vazaef.sazmanyar.Domain.Modles.ActionBudgetRequest
{
    public class ActionBudgetRequestEntity
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long BudgetRequestId { get; set; }
        public string BudgetAmountPeriod { get; set; }

        public RequestEntity BudgetRequest { get; set; }
    }
}
