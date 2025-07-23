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
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<RequestingDepartmen> RequestingDepartments { get; set; }
        public DbSet<FundingSource> FundingSources { get; set; }
        public DbSet<RequestEntity> Requests { get; set; }
        public DbSet<Allocation> Allocations { get; set; }
        public DbSet<AllocationActionBudgetRequest> AllocationActionBudgetRequests { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ارتباط Request ⇄ RequestingDepartmen
            modelBuilder.Entity<RequestEntity>()
                .HasOne(r => r.RequestingDepartment)
                .WithMany(rd => rd.Requests)
                .HasForeignKey(r => r.RequestingDepartmentId);

            // ارتباط Request ⇄ RequestType
            modelBuilder.Entity<RequestEntity>()
                .HasOne(r => r.RequestType)
                .WithMany(rt => rt.Requests)
                .HasForeignKey(r => r.RequestTypeId);

            // ارتباط Request ⇄ FundingSource
            modelBuilder.Entity<RequestEntity>()
                .HasOne(r => r.FundingSource)
                .WithMany(fs => fs.Requests)
                .HasForeignKey(r => r.FundingSourceId);

            // کلید مرکب برای جدول واسط AllocationActionBudgetRequest
            modelBuilder.Entity<AllocationActionBudgetRequest>()
                .HasKey(x => new { x.AllocationId, x.ActionBudgetRequestId });

            // ارتباط Allocation ⇄ AllocationActionBudgetRequest
            modelBuilder.Entity<AllocationActionBudgetRequest>()
                .HasOne(x => x.Allocation)
                .WithMany(x => x.AllocationActionBudgetRequests)
                .HasForeignKey(x => x.AllocationId)
                // حذف Cascade برای جلوگیری از Multiple cascade paths
                .OnDelete(DeleteBehavior.Restrict);

            // ارتباط ActionBudgetRequest ⇄ AllocationActionBudgetRequest
            modelBuilder.Entity<AllocationActionBudgetRequest>()
                .HasOne(x => x.ActionBudgetRequest)
                .WithMany()
                .HasForeignKey(x => x.ActionBudgetRequestId)
                .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<AllocationActionBudgetRequest>()
                .Property(p => p.AllocatedAmount)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaymentMethod)
                .WithMany()

                .HasForeignKey(p => p.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Allocation)
                .WithMany()
                .HasForeignKey(p => p.AllocationId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}