using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.RequestType
{
    // اینترفیس برای عملیات روی نوع درخواست‌ها
    public interface IRequestTypeRepository
    {
        Task<IEnumerable<RequestType>> GetAllAsync(); // دریافت لیست انواع درخواست‌ها
        Task<RequestType> GetByIdAsync(long id); // دریافت نوع خاص با شناسه
        Task AddAsync(RequestType entity); // افزودن نوع درخواست
        Task UpdateAsync(RequestType entity); // به‌روزرسانی نوع
        Task DeleteAsync(long id); // حذف نوع درخواست
    }
}