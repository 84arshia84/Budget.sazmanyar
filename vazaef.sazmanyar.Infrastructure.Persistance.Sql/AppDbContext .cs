using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.ActionBudgetRequest;
using vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;
using vazaef.sazmanyar.Domain.Modles.RequestType;
using FundingSource = vazaef.sazmanyar.Domain.Modles.PlaceOfFinancing.FundingSource;
using RequestEntity = vazaef.sazmanyar.Domain.Modles.Request.RequestEntity;
using RequestingDepartment = vazaef.sazmanyar.Domain.Modles.RequestingUnit.RequestingDepartment;
using RequestType = vazaef.sazmanyar.Domain.Modles.RequestType.RequestType;
using RequestModel = vazaef.sazmanyar.Domain.Modles.Request.RequestEntity;
using ActionBudgetRequestModel = vazaef.sazmanyar.Domain.Modles.ActionBudgetRequest.ActionBudgetRequestEntity;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<RequestingDepartment> RequestingDepartments { get; set; }
        public DbSet<FundingSource> FundingSources { get; set; }
        public DbSet<RequestEntity> Requests { get; set; }
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

            modelBuilder.Entity<ActionBudgetRequestEntity>()
                .HasOne(ab => ab.BudgetRequest)
                .WithMany(r => r.ActionBudgetRequests)
                .HasForeignKey(ab => ab.BudgetRequestId);
        }
    }
}

