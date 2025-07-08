using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;
using static System.Net.Mime.MediaTypeNames;

namespace vazaef.sazmanyar.Domain.Modles.Request
{
    public class Request
    {
        public long Id { get; set; }
        public string RequestTitle { get; set; }
        public long RequestingDepartmentId { get; set; }
        public long RequestTypeId { get; set; }
        public long FundingSourceId { get; set; }
        public int ApplicationYear { get; set; }
        public int TimeFrame { get; set; }
        public string ServiceDescription { get; set; }

        public RequestingDepartment RequestingDepartment { get; set; }
        public RequestType.RequestType RequestType { get; set; }
        public FundingSource FundingSource { get; set; }

    }
}


