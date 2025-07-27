using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing; // منبع مالی
using vazaef.sazmanyar.Domain.Modles.RequestingUnit; // واحد درخواست‌کننده
using static System.Net.Mime.MediaTypeNames;
using vazaef.sazmanyar.Domain.Modles.Request; // موجودیت درخواست
using RequestModel = vazaef.sazmanyar.Domain.Modles.Request.RequestEntity;
using vazaef.sazmanyar.Domain.Modles.ActionBudgetRequest;

namespace vazaef.sazmanyar.Domain.Modles.Request
{
    public class RequestEntity // موجودیت درخواست کلی
    {
        public long Id { get; set; } // شناسه درخواست
        public string RequestTitle { get; set; } // عنوان درخواست
        public long RequestingDepartmentId { get; set; } // واحد درخواست‌کننده
        public long RequestTypeId { get; set; } // نوع درخواست
        public long FundingSourceId { get; set; } // منبع مالی
        public int year { get; set; } // سال درخواست
        public string ServiceDescription { get; set; } // شرح خدمات مورد نظر
        public string budgetEstimationRanges { get; set; } // بازه تخمینی بودجه

        public RequestingUnit.RequestingDepartmen RequestingDepartment { get; set; } // موجودیت واحد درخواست‌کننده
        public RequestType.RequestType RequestType { get; set; } // نوع درخواست
        public PlaceOfFinancing.FundingSource FundingSource { get; set; } // منبع تأمین مالی

        public ICollection<ActionBudgetRequestEntity> ActionBudgetRequests { get; set; } = new List<ActionBudgetRequestEntity>(); // لیست درخواست‌های عملیاتی مرتبط
    }
}