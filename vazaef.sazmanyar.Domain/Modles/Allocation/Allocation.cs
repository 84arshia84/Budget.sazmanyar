using System; // قابلیت‌های پایه‌ای
using System.Collections.Generic; // استفاده از لیست‌ها
using System.Linq; // عملیات LINQ
using System.Text;
using System.Threading.Tasks;
using AllocationABRModel = vazaef.sazmanyar.Domain.Modles.AllocationActionBudgetRequest.AllocationActionBudgetRequest; // alias برای رابطه بین تخصیص و درخواست بودجه عملیاتی

namespace vazaef.sazmanyar.Domain.Modles.Allocation
{
    public class Allocation // کلاس تخصیص بودجه
    {
        public long Id { get; set; } // شناسه یکتا
        public string Title { get; set; } // عنوان تخصیص
        public DateTime Date { get; set; } // تاریخ تخصیص
        public long RequestId { get; set; } // شناسه درخواست مرتبط
        public Request.RequestEntity Request { get; set; } // ارتباط با موجودیت درخواست

        public List<AllocationABRModel> AllocationActionBudgetRequests { get; set; } = new(); // لیستی از روابط بین این تخصیص و درخواست‌های عملیاتی
    }
}