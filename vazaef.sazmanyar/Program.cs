
using Microsoft.EntityFrameworkCore;
using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Services;
using vazaef.sazmanyar.Application.Validators.Payment;
using vazaef.sazmanyar.Domain.Modles.Allocation;
using vazaef.sazmanyar.Domain.Modles.fundingSource;
using vazaef.sazmanyar.Domain.Modles.Payment;
using vazaef.sazmanyar.Domain.Modles.PaymentMethod;
using vazaef.sazmanyar.Domain.Modles.Request;
using vazaef.sazmanyar.Domain.Modles.RequestingUnit;
using vazaef.sazmanyar.Domain.Modles.RequestType;
using vazaef.sazmanyar.Infrastructure.Persistance.Sql;
using vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies;
using vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies;


namespace vazaef.sazmanyar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
                
            // ثبت DbContext و Repository و Service
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IRequestTypeRepository, RequestTypeRepository>();
            builder.Services.AddScoped<IRequestTypeService, RequestTypeService>();
            builder.Services.AddScoped<IFundingSourceRepository, FundingSourceRepository>();
            builder.Services.AddScoped<IRequestingDepartmenRepository, RequestingDepartmenRepository>();
            builder.Services.AddScoped<IFundingSourceService, FundingSourceService>();
            builder.Services.AddScoped<IRequestingDepartmenService, RequestingDepartmenService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<IRequestRepository, RequestRepository>();
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.Services.AddScoped<RequestRepository>();
            builder.Services.AddScoped<IAllocationRepository, AllocationRepository>();
            builder.Services.AddScoped<IAllocationService, AllocationService>();
            builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<CreatePaymentDtoValidator>();





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
