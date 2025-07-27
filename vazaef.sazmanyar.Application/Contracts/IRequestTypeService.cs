using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.RequestType;
using vazaef.sazmanyar.Domain.Modles.RequestType;

namespace vazaef.sazmanyar.Application.Contracts
{
    // اینترفیس سرویس انواع درخواست‌ها
    public interface IRequestTypeService
    {
        Task<IEnumerable<GetAllRequestTypeDto>> GetAllAsync(); // دریافت همه نوع‌های درخواست
        Task<GetByIdRequestTypeDto> GetByIdAsync(long id); // دریافت نوع خاص با شناسه
        Task AddAsync(AddRequestTypeDto dto); // ایجاد نوع جدید
        Task UpdateAsync(long id, UpdateRequestTypeDto dto); // به‌روزرسانی نوع درخواست
        Task DeleteAsync(long id); // حذف نوع درخواست
    }
}