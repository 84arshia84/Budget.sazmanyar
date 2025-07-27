using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.PaymentMethod
{
    public class PaymentMethod // روش‌های پرداخت مختلف
    {
        public long Id { get; set; } // شناسه
        public string Name { get; set; } // نام روش پرداخت (مثلاً نقدی، کارت، ...)
    }
}