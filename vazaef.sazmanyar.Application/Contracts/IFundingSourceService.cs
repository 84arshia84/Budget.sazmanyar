using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.FundingSource;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;

namespace vazaef.sazmanyar.Application.Contracts
{
    // اینترفیس سرویس منبع تأمین مالی
    public interface IFundingSourceService
    {
        Task<IEnumerable<GetAllFundingSourceDto>> GetAllAsync(); // دریافت لیست همه منابع تأمین مالی
        Task<GetByIdFundingSourceDto> GetByIdAsync(long id); // دریافت اطلاعات منبع خاص با شناسه
        Task AddAsync(AddFundingSourceDto dto); // افزودن منبع مالی جدید
        Task UpdateAsync(long id, UpdateFundingSourceDto dto); // به‌روزرسانی اطلاعات منبع مالی
        Task DeleteAsync(long id); // حذف منبع مالی
    }
}