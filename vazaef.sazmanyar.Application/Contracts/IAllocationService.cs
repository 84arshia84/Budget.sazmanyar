using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.Allocation;

namespace vazaef.sazmanyar.Application.Contracts
{
    // اینترفیس سرویس تخصیص بودجه
    public interface IAllocationService
    {
        Task AddAsync(CreateAllocationDto dto); // افزودن تخصیص جدید از طریق DTO

        Task UpdateAsync(long id, UpdateAllocationDto dto); // به‌روزرسانی تخصیص با استفاده از شناسه و اطلاعات جدید
        Task DeleteAsync(long id); // حذف تخصیص با شناسه
        Task<AllocationDto?> GetByIdAsync(long id); // دریافت اطلاعات تخصیص با شناسه
        Task<List<AllocationDto>> GetAllAsync(); // دریافت لیست همه تخصیص‌ها
    }
}