using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Application.Dto.AllocationPayment;
using vazaef.sazmanyar.Domain.Modles.ActionBudgetRequest;
using vazaef.sazmanyar.Domain.Modles.Allocation;
using vazaef.sazmanyar.Domain.Modles.AllocationActionBudgetRequest;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;
using vazaef.sazmanyar.Domain.Modles.RequestType;
using ActionBudgetRequestModel = vazaef.sazmanyar.Domain.Modles.ActionBudgetRequest.ActionBudgetRequestEntity;
using FundingSource = vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing.FundingSource;
using RequestEntity = vazaef.sazmanyar.Domain.Modles.Request.RequestEntity;
using RequestingDepartmen = vazaef.sazmanyar.Domain.Modles.RequestingUnit.RequestingDepartmen;
using RequestModel = vazaef.sazmanyar.Domain.Modles.Request.RequestEntity;
using RequestType = vazaef.sazmanyar.Domain.Modles.RequestType.RequestType;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<RequestingDepartmen> RequestingDepartments { get; set; }
        public DbSet<FundingSource> FundingSources { get; set; }
        public DbSet<RequestEntity> Requests { get; set; }
        public DbSet<Allocation> Allocations { get; set; }
        public DbSet<AllocationPayment> AllocationPayments { get; set; }
        public DbSet<AllocationActionBudgetRequest> AllocationActionBudgetRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API برای ارتباط‌ها
            modelBuilder.Entity<RequestEntity>()
                .HasOne(r => r.RequestingDepartment)
                .WithMany(rd => rd.Requests)
                .HasForeignKey(r => r.RequestingDepartmentId);

            modelBuilder.Entity<RequestEntity>()
                .HasOne(r => r.RequestType)
                .WithMany(rt => rt.Requests)
                .HasForeignKey(r => r.RequestTypeId);

            modelBuilder.Entity<RequestEntity>()
                .HasOne(r => r.FundingSource)
                .WithMany(fs => fs.Requests)
                .HasForeignKey(r => r.FundingSourceId);

            modelBuilder.Entity<AllocationActionBudgetRequest>()
     .HasKey(x => new { x.AllocationId, x.ActionBudgetRequestId });

            modelBuilder.Entity<AllocationActionBudgetRequest>()
                .HasOne(x => x.Allocation)
                .WithMany(x => x.AllocationActionBudgetRequests)
                .HasForeignKey(x => x.AllocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AllocationActionBudgetRequest>()
                .HasOne(x => x.ActionBudgetRequest)
                .WithMany()
                .HasForeignKey(x => x.ActionBudgetRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AllocationPayment>()
                .Property(p => p.PaidAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<AllocationActionBudgetRequest>()
                .Property(p => p.AllocatedAmount)
                .HasPrecision(18, 2);
        }

    }
}
