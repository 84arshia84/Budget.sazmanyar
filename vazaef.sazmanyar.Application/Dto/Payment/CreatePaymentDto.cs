using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Application.Dto.Payment
{
    public class CreatePaymentDto
    {
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public long AllocationId { get; set; }
        public long PaymentMethodId { get; set; }
    }
}
