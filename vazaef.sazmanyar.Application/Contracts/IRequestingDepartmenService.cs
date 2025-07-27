using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.RequestingDepartmen;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;

namespace vazaef.sazmanyar.Application.Contracts
{
    // اینترفیس سرویس دپارتمان‌های درخواست‌کننده
    public interface IRequestingDepartmenService
    {
        Task<IEnumerable<GetAllRequestingDepartmenDto>> GetAllAsync(); // دریافت لیست دپارتمان‌ها
        Task<GetByIdRequestingDepartmenDto> GetByIdAsync(long id); // دریافت دپارتمان با شناسه
        Task AddAsync(AddRequestingDepartmenDto dto); // افزودن دپارتمان جدید
        Task UpdateAsync(long id, UpdateRequestingDepartmenDto dto); // به‌روزرسانی اطلاعات دپارتمان
        Task DeleteAsync(long id); // حذف دپارتمان
    }
}