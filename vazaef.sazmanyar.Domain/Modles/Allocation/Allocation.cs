    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AllocationABRModel = vazaef.sazmanyar.Domain.Modles.AllocationActionBudgetRequest.AllocationActionBudgetRequest;
    namespace vazaef.sazmanyar.Domain.Modles.Allocation
    {
        public class Allocation
        {
            public long Id { get; set; }
            public string Title { get; set; }
            public DateTime Date { get; set; }
            public long RequestId { get; set; }
            public Request.RequestEntity Request { get; set; }

            public List<AllocationABRModel> AllocationActionBudgetRequests { get; set; } = new();
        }
    }

