using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using vazaef.sazmanyar.Domain.Modles.ActionBudgetRequest;
using vazaef.sazmanyar.Domain.Modles.Allocation;
using vazaef.sazmanyar.Domain.Modles.AllocationActionBudgetRequest;
using vazaef.sazmanyar.Domain.Modles.Payment;
using vazaef.sazmanyar.Domain.Modles.PaymentMethod;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;
using vazaef.sazmanyar.Domain.Modles.Request;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;
using vazaef.sazmanyar.Domain.Modles.RequestType;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql
{
    public class AppDbContext : DbContext
    {
        // کانستراکتور با دریافت گزینه‌های کانفیگ از DI
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // تعریف DbSet ها که نماینده جداول دیتابیس هستند
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<RequestingDepartmen> RequestingDepartments { get; set; }
        public DbSet<FundingSource> FundingSources { get; set; }
        public DbSet<RequestEntity> Requests { get; set; }
        public DbSet<Allocation> Allocations { get; set; }
        public DbSet<AllocationActionBudgetRequest> AllocationActionBudgetRequests { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Payment> Payments { get; set; }

        // متد برای پیکربندی مدل و روابط بین جداول در EF Core
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // تعریف رابطه یک به چند بین Request و RequestingDepartmen
            // یک Request به یک RequestingDepartmen تعلق دارد، و یک RequestingDepartmen می‌تواند چند Request داشته باشد
            modelBuilder.Entity<RequestEntity>()
                .HasOne(r => r.RequestingDepartment)
                .WithMany(rd => rd.Requests)
                .HasForeignKey(r => r.RequestingDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);


            // تعریف رابطه یک به چند بین Request و RequestType
            modelBuilder.Entity<RequestEntity>()
                .HasOne(r => r.RequestType)
                .WithMany(rt => rt.Requests)
                .HasForeignKey(r => r.RequestTypeId)
                .OnDelete(DeleteBehavior.Restrict);


            // تعریف رابطه یک به چند بین Request و FundingSource (منبع تامین مالی)
            modelBuilder.Entity<RequestEntity>()
                .HasOne(r => r.FundingSource)
                .WithMany(fs => fs.Requests)
                .HasForeignKey(r => r.FundingSourceId)
                .OnDelete(DeleteBehavior.Restrict);

            // تعریف کلید مرکب برای جدول واسط AllocationActionBudgetRequest
            modelBuilder.Entity<AllocationActionBudgetRequest>()
                .HasKey(x => new { x.AllocationId, x.ActionBudgetRequestId });

            // تعریف رابطه Allocation به AllocationActionBudgetRequest با کلید خارجی AllocationId
            // یک Allocation می‌تواند چند AllocationActionBudgetRequest داشته باشد
            modelBuilder.Entity<AllocationActionBudgetRequest>()
                .HasOne(x => x.Allocation)
                .WithMany(x => x.AllocationActionBudgetRequests)
                .HasForeignKey(x => x.AllocationId)
                // حذف Cascade برای جلوگیری از Multiple cascade paths که باعث خطا می‌شود
                .OnDelete(DeleteBehavior.Restrict);

            // تعریف رابطه ActionBudgetRequest به AllocationActionBudgetRequest
            // هر AllocationActionBudgetRequest به یک ActionBudgetRequest وابسته است
            modelBuilder.Entity<AllocationActionBudgetRequest>()
                .HasOne(x => x.ActionBudgetRequest)
                .WithMany()
                .HasForeignKey(x => x.ActionBudgetRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // تنظیم دقت و مقیاس عددی برای ستون AllocatedAmount (مقدار تخصیص یافته)
            modelBuilder.Entity<AllocationActionBudgetRequest>()
                .Property(p => p.AllocatedAmount)
                .HasPrecision(18, 2);

            // فراخوانی متد پایه
            base.OnModelCreating(modelBuilder);

            // تعریف رابطه بین Payment و PaymentMethod با جلوگیری از حذف Cascade
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaymentMethod)
                .WithMany()
                .HasForeignKey(p => p.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict); // جلوگیری از حذف PaymentMethod در صورت وجود Payment

            // تعریف رابطه بین Payment و Allocation
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Allocation)
                .WithMany()
                .HasForeignKey(p => p.AllocationId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Allocation>()
                .HasOne(a => a.Request)
                .WithMany()
                .HasForeignKey(a => a.RequestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
