using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.Request;
using vazaef.sazmanyar.Domain.Modles.Request;

namespace vazaef.sazmanyar.Application.Contracts
{
    // اینترفیس سرویس مدیریت درخواست‌ها
    public interface IRequestService
    {
        Task<GetRequestByIdDto> GetByIdAsync(long id); // دریافت اطلاعات یک درخواست خاص
        Task AddAsync(CreateRequestDto dto); // ایجاد درخواست جدید
        Task<bool> UpdateAsync(long id, EditRequestDto dto); // به‌روزرسانی درخواست
        Task<bool> DeleteAsync(long id); // حذف درخواست با شناسه
        Task<IEnumerable<GetAllRequestDto>> GetAllWithTotalBudgetAsync(); // دریافت تمام درخواست‌ها با مجموع بودجه
        Task<IEnumerable<GetRequestByIdsDto>> GetByIdsAsync(IEnumerable<long> ids); // دریافت درخواست‌ها بر اساس چند شناسه
        Task<List<RequestDto>> GetAllWithBudgetEstimationAsync(); // دریافت همه درخواست‌ها به همراه تخمین بودجه
    }
}