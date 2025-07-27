using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.RequestingUnit
{
    public class RequestingDepartmen // واحد یا دپارتمان درخواست‌کننده
    {
        public long Id { get; set; } // شناسه
        public string Description { get; set; } // توضیح دپارتمان

        public ICollection<Request.RequestEntity> Requests { get; set; } // لیست درخواست‌هایی که توسط این دپارتمان ثبت شده
    }
}