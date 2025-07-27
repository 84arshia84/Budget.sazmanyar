using System; // استفاده از فضای نام System برای قابلیت‌های پایه‌ای
using System.Collections.Generic; // برای استفاده از لیست‌ها و مجموعه‌ها
using System.Linq; // برای عملیات LINQ
using System.Text; // برای عملیات روی رشته‌ها
using System.Threading.Tasks; // برای استفاده از Task و async/await
using vazaef.sazmanyar.Domain.Modles.Request; // ارجاع به موجودیت Request
using ActionBudgetRequestModel = vazaef.sazmanyar.Domain.Modles.ActionBudgetRequest.ActionBudgetRequestEntity; // ایجاد alias برای موجودیت ActionBudgetRequest
using RequestModel = vazaef.sazmanyar.Domain.Modles.Request.RequestEntity; // ایجاد alias برای موجودیت RequestEntity

namespace vazaef.sazmanyar.Domain.Modles.ActionBudgetRequest // تعریف فضای نام مربوط به درخواست‌های بودجه‌ای
{
    public class ActionBudgetRequestEntity // تعریف کلاس برای نگهداری اطلاعات درخواست بودجه عملیاتی
    {
        public long Id { get; set; } // شناسه یکتا
        public string Title { get; set; } // عنوان درخواست بودجه
        public long BudgetRequestId { get; set; } // شناسه درخواست اصلی بودجه
        public string BudgetAmountPeriod { get; set; } // بازه مبلغ بودجه

        public RequestEntity BudgetRequest { get; set; } // شیء مرتبط با موجودیت Request
    }
}