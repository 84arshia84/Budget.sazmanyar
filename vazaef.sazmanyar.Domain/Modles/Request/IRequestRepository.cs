using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.Request
{
    // اینترفیس برای مدیریت درخواست‌ها
    public interface IRequestRepository
    {
        //Task<IEnumerable<RequestEntity>> GetAllAsync(); // دریافت تمام درخواست‌ها (غیرفعال شده)

        Task<RequestEntity> GetByIdAsync(long id); // دریافت درخواست خاص با شناسه
        Task AddAsync(RequestEntity request); // افزودن درخواست جدید
        Task UpdateAsync(RequestEntity request); // به‌روزرسانی اطلاعات درخواست
        Task DeleteAsync(long id); // حذف درخواست

        Task<string> GetAllRequestsWithTotalBudgetJsonAsync(); // دریافت لیست درخواست‌ها با مجموع بودجه به صورت JSON
        Task<string> GetRequestsByIdsWithTotalBudgetJsonAsync(IEnumerable<long> ids); // دریافت درخواست‌های انتخابی با مجموع بودجه به صورت JSON

        Task<List<RequestEntity>> GetAllWithActionBudgetRequestsAsync(); // دریافت تمام درخواست‌ها همراه با درخواست‌های عملیاتی بودجه
    }
}