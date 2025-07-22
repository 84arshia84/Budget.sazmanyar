using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.Payment
{
    public class Payment
    {
        public long Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public long AllocationId { get; set; }
        public long PaymentMethodId { get; set; }

        public Allocation.Allocation Allocation { get; set; }
        public PaymentMethod.PaymentMethod PaymentMethod { get; set; }
    }
}
