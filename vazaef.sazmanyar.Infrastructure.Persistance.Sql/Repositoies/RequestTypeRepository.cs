using Microsoft.EntityFrameworkCore;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.RequestType;
using vazaef.sazmanyar.Domain.Modles.Allocation;
using vazaef.sazmanyar.Domain.Modles.RequestType;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    public class RequestTypeRepository : IRequestTypeRepository
    {
        private readonly AppDbContext _context;

        // دریافت کانتکست از DI
        public RequestTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        // گرفتن همه انواع درخواست‌ها از دیتابیس
        public async Task<IEnumerable<RequestType>> GetAllAsync()
        {
            return await _context.RequestTypes.ToListAsync();
        }

        // گرفتن یک نوع درخواست بر اساس شناسه
        public async Task<RequestType> GetByIdAsync(long id)
        {
            return await _context.RequestTypes.FindAsync(id);
        }

        // اضافه کردن یک نوع درخواست جدید
        public async Task AddAsync(RequestType entity)
        {
            await _context.RequestTypes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // به‌روزرسانی یک نوع درخواست موجود به همراه درخواست‌های مرتبط
        public async Task UpdateAsync(RequestType requestType)
        {
            // بارگذاری نوع درخواست همراه با مجموعه درخواست‌ها
            var existing = await _context.RequestTypes
                .Include(rt => rt.Requests)
                .FirstOrDefaultAsync(rt => rt.Id == requestType.Id);

            if (existing == null)
                throw new KeyNotFoundException($"RequestType with Id={requestType.Id} not found.");

            existing.Description = requestType.Description;

            // پاکسازی درخواست‌های قبلی و افزودن درخواست‌های جدید در صورت وجود
            if (requestType.Requests != null && requestType.Requests.Any())
            {
                existing.Requests.Clear();
                foreach (var req in requestType.Requests)
                {
                    existing.Requests.Add(req);
                }
            }

            await _context.SaveChangesAsync();
        }

        // حذف نوع درخواست با شناسه مشخص
        public async Task DeleteAsync(long id)
        {
            var entity = await _context.RequestTypes.FindAsync(id);
            if (entity != null)
            {
                _context.RequestTypes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
