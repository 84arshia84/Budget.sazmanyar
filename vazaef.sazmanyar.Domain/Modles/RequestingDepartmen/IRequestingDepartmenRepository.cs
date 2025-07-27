using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.RequestingUnit
{
    // اینترفیس برای عملیات واحدهای درخواست‌کننده
    public interface IRequestingDepartmenRepository
    {
        Task<IEnumerable<RequestingDepartmen>> GetAllAsync(); // دریافت لیست تمام دپارتمان‌ها
        Task<RequestingDepartmen> GetByIdAsync(long id); // دریافت دپارتمان خاص
        Task AddAsync(RequestingDepartmen entity); // افزودن دپارتمان جدید
        Task UpdateAsync(RequestingDepartmen entity); // به‌روزرسانی دپارتمان
        Task DeleteAsync(long id); // حذف دپارتمان
    }
}