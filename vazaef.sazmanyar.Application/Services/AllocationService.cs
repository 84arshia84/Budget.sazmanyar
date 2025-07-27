// این کلاس وظیفه مدیریت عملیات تخصیص بودجه را دارد (Create, Update, Delete, Get)
// نام این الگوی معماری Service Layer است که بین لایه Domain و UI قرار دارد.
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Dto.Allocation;
using vazaef.sazmanyar.Domain.Modles.Allocation;
using vazaef.sazmanyar.Domain.Modles.AllocationActionBudgetRequest;

public class AllocationService : IAllocationService
{
    // با استفاده از Dependency Injection، ما Repository را تزریق می‌کنیم تا به داده‌ها دسترسی داشته باشیم.
    private readonly IAllocationRepository _repo;

    public AllocationService(IAllocationRepository repo)
    {
        _repo = repo;
    }

    // متدی برای افزودن یک تخصیص جدید (allocation)
    public async Task AddAsync(CreateAllocationDto dto)
    {
        // ساخت آبجکت از نوع Domain Model با استفاده از داده‌های ورودی (DTO)
        var allocation = new Allocation
        {
            Title = dto.AllocationTitle, // عنوان تخصیص
            Date = dto.AllocationDate,   // تاریخ تخصیص
            RequestId = dto.BudgetRequestId, // آیدی درخواست بودجه

            // تبدیل لیست تخصیص‌های عملیاتی به مدل دامنه‌ای
            AllocationActionBudgetRequests = dto.ActionAllocations
                .Select(a => new AllocationActionBudgetRequest
                {
                    ActionBudgetRequestId = a.ActionBudgetRequestId,
                    AllocatedAmount = a.AllocatedAmount
                }).ToList()
        };

        // ذخیره آبجکت ساخته شده از طریق Repository
        await _repo.AddAsync(allocation);
    }

    // متدی برای به‌روزرسانی تخصیص موجود
    public async Task UpdateAsync(long id, UpdateAllocationDto dto)
    {
        // مرحله اول: گرفتن تخصیص فعلی از دیتابیس
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Allocation {id} not found.");

        // ساخت یک آبجکت جدید با داده‌های به‌روز شده
        var updated = new Allocation
        {
            Id = id,
            Title = dto.AllocationTitle,
            Date = dto.AllocationDate,
            RequestId = dto.BudgetRequestId,

            AllocationActionBudgetRequests = dto.ActionAllocations
                .Select(x => new AllocationActionBudgetRequest
                {
                    ActionBudgetRequestId = x.ActionBudgetRequestId,
                    AllocatedAmount = x.AllocatedAmount
                }).ToList()
        };

        // ارسال داده به Repository برای به‌روزرسانی در پایگاه داده
        await _repo.UpdateAsync(updated);
    }

    // حذف یک تخصیص بر اساس ID
    public async Task DeleteAsync(long id) => await _repo.DeleteAsync(id);

    // دریافت اطلاعات یک تخصیص خاص بر اساس ID
    public async Task<AllocationDto?> GetByIdAsync(long id)
    {
        // گرفتن اطلاعات از دیتابیس
        var allocation = await _repo.GetByIdAsync(id);
        if (allocation == null) return null;

        // تبدیل داده از مدل دامنه به DTO برای ارسال به UI
        return new AllocationDto
        {
            Id = allocation.Id,
            Title = allocation.Title,
            Date = allocation.Date,
            RequestId = allocation.RequestId,
            ActionAllocations = allocation.AllocationActionBudgetRequests
                .Select(a => new ActionAllocationDto
                {
                    ActionBudgetRequestId = a.ActionBudgetRequestId,
                    AllocatedAmount = a.AllocatedAmount
                }).ToList()
        };
    }

    // گرفتن لیست تمام تخصیص‌ها برای نمایش کلی
    public async Task<List<AllocationDto>> GetAllAsync()
    {
        var allocations = await _repo.GetAllAsync();

        // تبدیل لیست از Domain Model به DTO
        return allocations.Select(allocation => new AllocationDto
        {
            Id = allocation.Id,
            Title = allocation.Title,
            Date = allocation.Date,
            RequestId = allocation.RequestId,
            ActionAllocations = allocation.AllocationActionBudgetRequests
                .Select(a => new ActionAllocationDto
                {
                    ActionBudgetRequestId = a.ActionBudgetRequestId,
                    AllocatedAmount = a.AllocatedAmount
                }).ToList()
        }).ToList();
    }
}