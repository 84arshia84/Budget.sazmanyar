using System; // پایه‌ای
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllocationModel = vazaef.sazmanyar.Domain.Modles.Allocation.Allocation; // alias

namespace vazaef.sazmanyar.Domain.Modles.AllocationActionBudgetRequest
{
    public class AllocationActionBudgetRequest // رابطه بین تخصیص و درخواست بودجه عملیاتی
    {
        public long AllocationId { get; set; } // شناسه تخصیص
        public AllocationModel Allocation { get; set; } // موجودیت تخصیص مرتبط
        public long ActionBudgetRequestId { get; set; } // شناسه درخواست عملیاتی بودجه
        public ActionBudgetRequest.ActionBudgetRequestEntity ActionBudgetRequest { get; set; } // موجودیت مربوط به درخواست عملیاتی
        public decimal AllocatedAmount { get; set; } // مبلغ تخصیص داده شده
    }
}