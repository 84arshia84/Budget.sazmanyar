using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.Allocation;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    public class AllocationRepository : IAllocationRepository
    {
        private readonly AppDbContext _context;

        public AllocationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Allocation>> GetAllAsync() =>
            await _context.Allocations
                .Include(a => a.AllocationActionBudgetRequests)
                .Include(a => a.Payments)
                .ToListAsync();

        public async Task<Allocation?> GetByIdAsync(long id) =>
            await _context.Allocations
                .Include(a => a.AllocationActionBudgetRequests)
                .Include(a => a.Payments)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task AddAsync(Allocation allocation)
        {
            await _context.Allocations.AddAsync(allocation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Allocation allocation)
        {
            // 1. بارگذاری Allocation همراه با مجموعه‌ی پیوت
            var existing = await _context.Allocations
                .Include(a => a.AllocationActionBudgetRequests)
                .FirstOrDefaultAsync(a => a.Id == allocation.Id);

            if (existing == null)
                throw new KeyNotFoundException($"Allocation with Id={allocation.Id} not found.");

            // 2. به‌روزرسانی فیلدهای ساده
            existing.Title = allocation.Title;
            existing.Date = allocation.Date;
            existing.RequestId = allocation.RequestId;

            // 3. حذف آیتم‌های قدیمی پیوت
            _context.AllocationActionBudgetRequests
                .RemoveRange(existing.AllocationActionBudgetRequests);

            // 4. افزودن آیتم‌های جدید پیوت (به existing.Entity)
            foreach (var abr in allocation.AllocationActionBudgetRequests)
            {
                // حتماً AllocationId و Allocation navigation به existing اشاره کند
                abr.AllocationId = existing.Id;
                abr.Allocation = existing;
                _context.AllocationActionBudgetRequests.Add(abr);
            }

            // 5. ذخیره نهایی
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            // 1. بارگذاری Allocation همراه با آیتم‌های واسط
            var entity = await _context.Allocations
                .Include(a => a.AllocationActionBudgetRequests)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (entity != null)
            {
                // 2. حذف رکوردهای واسط (AllocationActionBudgetRequests)
                _context.AllocationActionBudgetRequests.RemoveRange(entity.AllocationActionBudgetRequests);
                await _context.SaveChangesAsync();

                // 3. حالا خود Allocation را حذف کن
                _context.Allocations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

    }
}
