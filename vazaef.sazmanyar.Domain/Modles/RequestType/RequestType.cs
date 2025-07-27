using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.RequestType
{
    public class RequestType // نوع درخواست
    {
        public long Id { get; set; } // شناسه نوع درخواست
        public string Description { get; set; } // توضیح نوع

        public ICollection<Request.RequestEntity> Requests { get; set; } // لیست درخواست‌های با این نوع
    }
}