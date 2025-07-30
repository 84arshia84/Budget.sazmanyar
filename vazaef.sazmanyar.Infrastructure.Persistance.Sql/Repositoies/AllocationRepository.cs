using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.Allocation;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    // ریپازیتوری برای مدیریت داده‌های تخصیص (Allocation)
    // شامل عملیات CRUD و مدیریت ارتباط‌ها با جدول‌های پیوسته (Pivot Tables)
    public class AllocationRepository : IAllocationRepository
    {
        private readonly AppDbContext _context;

        // تزریق کانتکست دیتابیس در سازنده جهت دسترسی به داده‌ها
        public AllocationRepository(AppDbContext context)
        {
            _context = context;
        }

        // دریافت همه تخصیص‌ها همراه با آیتم‌های مربوط به بودجه‌های تخصیص یافته (پیوت)
        public async Task<IEnumerable<Allocation>> GetAllAsync() =>
            await _context.Allocations
                .Include(a => a.AllocationActionBudgetRequests)  // بارگذاری داده‌های مرتبط
                .ToListAsync();

        // دریافت تخصیص خاص بر اساس شناسه به همراه داده‌های پیوسته
        public async Task<Allocation?> GetByIdAsync(long id) =>
            await _context.Allocations
                .Include(a => a.AllocationActionBudgetRequests)
                .FirstOrDefaultAsync(a => a.Id == id);

        // افزودن تخصیص جدید به دیتابیس
        public async Task AddAsync(Allocation allocation)
        {
            await _context.Allocations.AddAsync(allocation);
            await _context.SaveChangesAsync(); // ✅ این خط حتماً اجرا شود
        }



        // به‌روزرسانی تخصیص موجود همراه با مدیریت کامل آیتم‌های پیوسته (Pivot)
        public async Task UpdateAsync(Allocation allocation)
        {
            _context.Allocations.Update(allocation);
            await _context.SaveChangesAsync();
        }

        // حذف تخصیص به همراه داده‌های مرتبط (پیوت) برای حفظ یکپارچگی داده‌ها
        public async Task DeleteAsync(long id)
        {
            // 1. بارگذاری تخصیص به همراه داده‌های پیوسته
            var entity = await _context.Allocations
                .Include(a => a.AllocationActionBudgetRequests)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (entity != null)
            {
                // 2. حذف آیتم‌های مربوط به تخصیص در جدول واسط
                _context.AllocationActionBudgetRequests.RemoveRange(entity.AllocationActionBudgetRequests);
                await _context.SaveChangesAsync();

                // 3. حذف رکورد تخصیص اصلی
                _context.Allocations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

    }
}
