using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;
using vazaef.sazmanyar.Domain.Modles.RequestType;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    public class RequestingDepartmenRepository : IRequestingDepartmenRepository
    {
        private readonly AppDbContext _context;

        // دریافت context از طریق Dependency Injection
        public RequestingDepartmenRepository(AppDbContext context)
        {
            _context = context;
        }

        // گرفتن همه درخواست‌دهنده‌ها (RequestingDepartments) از دیتابیس به صورت async
        public async Task<IEnumerable<RequestingDepartmen>> GetAllAsync()
        {
            return await _context.RequestingDepartments.ToListAsync();
        }

        // گرفتن یک درخواست‌دهنده با شناسه مشخص، به صورت async
        public async Task<RequestingDepartmen> GetByIdAsync(long id)
        {
            return await _context.RequestingDepartments.FindAsync(id);
        }

        // اضافه کردن یک RequestingDepartmen جدید به دیتابیس
        public async Task AddAsync(RequestingDepartmen requestingDepartment)
        {
            await _context.RequestingDepartments.AddAsync(requestingDepartment);
            await _context.SaveChangesAsync();
        }

        // به‌روزرسانی یک RequestingDepartmen موجود همراه با به‌روزرسانی درخواست‌های مرتبط
        public async Task UpdateAsync(RequestingDepartmen requestingDepartment)
        {
            // بارگذاری موجودیت فعلی همراه با مجموعه درخواست‌ها (Requests)
            var existing = await _context.RequestingDepartments
                .Include(rt => rt.Requests)
                .FirstOrDefaultAsync(rt => rt.Id == requestingDepartment.Id);

            if (existing == null)
                throw new KeyNotFoundException($"requestingDepartment with Id={requestingDepartment.Id} not found.");

            // به‌روزرسانی فیلدهای ساده
            existing.Description = requestingDepartment.Description;

            // اگر درخواست‌های جدیدی وجود دارد، ابتدا لیست قبلی را پاک کن و سپس جدیدها را اضافه کن
            if (requestingDepartment.Requests != null && requestingDepartment.Requests.Any())
            {
                existing.Requests.Clear();
                foreach (var req in requestingDepartment.Requests)
                {
                    existing.Requests.Add(req);
                }
            }

            await _context.SaveChangesAsync();
        }

        // حذف RequestingDepartmen با شناسه مشخص
        public async Task DeleteAsync(long id)
        {
            var requestingDepartment = await _context.RequestingDepartments.FindAsync(id);
            if (requestingDepartment != null)
            {
                _context.RequestingDepartments.Remove(requestingDepartment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
