using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Application.Dto.AllocationPayment
{
   public class AllocationPayment
    {
        public long Id { get; set; }
        public long AllocationId { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime PaidDate { get; set; }
        public string PaymentType { get; set; }
    }
}
