using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;

namespace vazaef.sazmanyar.Domain.Modles.fundingSource
{
    // اینترفیس برای عملیات CRUD روی منابع تأمین مالی
    public interface IFundingSourceRepository
    {
        Task<IEnumerable<FundingSource>> GetAllAsync(); // دریافت همه منابع تأمین مالی
        Task<FundingSource> GetByIdAsync(long id); // دریافت منبع خاص با شناسه
        Task AddAsync(FundingSource fundingSource); // افزودن منبع جدید
        Task UpdateAsync(FundingSource fundingSource); // به‌روزرسانی اطلاعات منبع
        Task DeleteAsync(long id); // حذف منبع با شناسه
    }
}