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
            await _context.SaveChangesAsync(); // ذخیره تغییرات در دیتابیس
        }

        // به‌روزرسانی تخصیص موجود همراه با مدیریت کامل آیتم‌های پیوسته (Pivot)
        public async Task UpdateAsync(Allocation allocation)
        {
            // 1. بارگذاری تخصیص به همراه داده‌های پیوسته برای اصلاح
            var existing = await _context.Allocations
                .Include(a => a.AllocationActionBudgetRequests)
                .FirstOrDefaultAsync(a => a.Id == allocation.Id);

            // اگر تخصیص پیدا نشد، خطا صادر می‌شود
            if (existing == null)
                throw new KeyNotFoundException($"Allocation with Id={allocation.Id} not found.");

            // 2. به‌روزرسانی فیلدهای ساده و بدون ارتباط
            existing.Title = allocation.Title;
            existing.Date = allocation.Date;
            existing.RequestId = allocation.RequestId;

            // 3. حذف کامل آیتم‌های قدیمی از جدول پیوسته (برای اجتناب از ناسازگاری داده)
            _context.AllocationActionBudgetRequests.RemoveRange(existing.AllocationActionBudgetRequests);

            // 4. افزودن آیتم‌های جدید پیوسته به موجودیت بارگذاری شده
            foreach (var abr in allocation.AllocationActionBudgetRequests)
            {
                abr.AllocationId = existing.Id;    // تنظیم کلید خارجی به موجودیت به‌روزشده
                abr.Allocation = existing;         // تنظیم navigation property برای EF Core
                _context.AllocationActionBudgetRequests.Add(abr);
            }

            // 5. ذخیره تغییرات نهایی در دیتابیس
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
