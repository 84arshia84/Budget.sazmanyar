using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.RequestType
{
    public class RequestType
    {
        public long Id { get; set; }
        public string Description { get; set; }

        public ICollection<Request.RequestEntity> Requests { get; set; }
    }
}
