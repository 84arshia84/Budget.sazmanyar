using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.fundingSource;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    // ریپازیتوری مدیریت منبع تأمین مالی (FundingSource)
    public class FundingSourceRepository : IFundingSourceRepository
    {
        private readonly AppDbContext _context;

        public FundingSourceRepository(AppDbContext context)
        {
            _context = context;
        }

        // دریافت همه منابع تأمین مالی
        public async Task<IEnumerable<FundingSource>> GetAllAsync()
        {
            return await _context.FundingSources.ToListAsync();
        }

        // دریافت منبع تأمین مالی بر اساس شناسه
        public async Task<FundingSource> GetByIdAsync(long id)
        {
            return await _context.FundingSources.FindAsync(id);
        }

        // افزودن منبع تأمین مالی جدید به دیتابیس
        public async Task AddAsync(FundingSource fundingSource)
        {
            await _context.FundingSources.AddAsync(fundingSource);
            await _context.SaveChangesAsync();
        }

        // به‌روزرسانی منبع تأمین مالی به همراه مدیریت مجموعه درخواست‌های مرتبط
        public async Task UpdateAsync(FundingSource fundingSource)
        {
            // بارگذاری موجودیت همراه با مجموعه درخواست‌ها برای اصلاح دقیق
            var existing = await _context.FundingSources
                .Include(rt => rt.Requests) // بارگذاری Navigation Property مرتبط با درخواست‌ها
                .FirstOrDefaultAsync(rt => rt.Id == fundingSource.Id);

            if (existing == null)
                throw new KeyNotFoundException($"FundingSource with Id={fundingSource.Id} not found.");

            // به‌روزرسانی توضیحات
            existing.Description = fundingSource.Description;

            // اگر درخواست‌هایی به مدل ارسال شده ضمیمه شده‌اند، ابتدا کل درخواست‌های موجود پاک شده و درخواست‌های جدید اضافه می‌شود
            if (fundingSource.Requests != null && fundingSource.Requests.Any())
            {
                existing.Requests.Clear();
                foreach (var req in fundingSource.Requests)
                {
                    existing.Requests.Add(req);
                }
            }

            await _context.SaveChangesAsync();
        }

        // حذف منبع تأمین مالی در صورت وجود
        public async Task DeleteAsync(long id)
        {
            var fundingSource = await _context.FundingSources.FindAsync(id);
            if (fundingSource != null)
            {
                _context.FundingSources.Remove(fundingSource);
                await _context.SaveChangesAsync();
            }
        }
    }
}
