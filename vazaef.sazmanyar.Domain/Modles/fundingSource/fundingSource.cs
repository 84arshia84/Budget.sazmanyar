using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing
{
    public class FundingSource
    {
        public long Id { get; set; }
        public string Description { get; set; }

        public ICollection<Request.Request> Requests { get; set; }
    }
}