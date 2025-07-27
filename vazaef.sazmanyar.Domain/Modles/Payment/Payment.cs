using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.Payment
{
    public class Payment // پرداخت مرتبط با تخصیص
    {
        public long Id { get; set; } // شناسه پرداخت
        public DateTime PaymentDate { get; set; } // تاریخ پرداخت
        public decimal PaymentAmount { get; set; } // مبلغ پرداخت
        public long AllocationId { get; set; } // تخصیص مرتبط
        public long PaymentMethodId { get; set; } // روش پرداخت

        public Allocation.Allocation Allocation { get; set; } // موجودیت تخصیص
        public PaymentMethod.PaymentMethod PaymentMethod { get; set; } // موجودیت روش پرداخت
    }
}   