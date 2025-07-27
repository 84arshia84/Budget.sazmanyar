using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vazaef.sazmanyar.Domain.Modles.Allocation
{
    // اینترفیس مربوط به عملیات پایگاه داده برای تخصیص‌ها
    public interface IAllocationRepository
    {
        Task<IEnumerable<Allocation>> GetAllAsync(); // دریافت همه تخصیص‌ها
        Task<Allocation?> GetByIdAsync(long id); // دریافت یک تخصیص بر اساس شناسه
        Task AddAsync(Allocation allocation); // افزودن یک تخصیص جدید
        Task UpdateAsync(Allocation allocation); // به‌روزرسانی اطلاعات تخصیص
        Task DeleteAsync(long id); // حذف تخصیص با شناسه
    }
}