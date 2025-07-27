using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing
{
    public class FundingSource // منبع تأمین مالی
    {
        public long Id { get; set; } // شناسه
        public string Description { get; set; } // توضیح منبع تأمین مالی

        public ICollection<Request.RequestEntity> Requests { get; set; } // لیست درخواست‌هایی که از این منبع استفاده کرده‌اند
    }
}