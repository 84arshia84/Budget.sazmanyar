using Microsoft.EntityFrameworkCore;
// استفاده از فضای نام EF Core برای کار با دیتابیس و DbContext

using vazaef.sazmanyar.Application.Contracts;
using vazaef.sazmanyar.Application.Services;
using vazaef.sazmanyar.Application.Validators.Allocation;
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
// وارد کردن نام‌فضاهای مرتبط با لایه‌های Domain، Application و Infrastructure برای استفاده در DI و EF Core

namespace vazaef.sazmanyar
{
    public class Program
    {
        // نقطه ورود برنامه
        public static void Main(string[] args)
        {
            // ساخت یک WebApplicationBuilder برای کانفیگ برنامه و سرویس‌ها
            var builder = WebApplication.CreateBuilder(args);

            // ثبت سرویس‌ها و اجزای مختلف در Container تزریق وابستگی (Dependency Injection)

            // ثبت سرویس‌های کنترلر که وظیفه دریافت درخواست‌ها و پاسخ به آن‌ها را دارند
            builder.Services.AddControllers();

            // فعال‌سازی Endpoint API Explorer که برای مستندسازی API و کشف Endpointها کاربرد دارد
            builder.Services.AddEndpointsApiExplorer();

            // ثبت Swagger جهت ایجاد مستندات قابل خواندن توسط ماشین و UI جذاب برای تست API
            builder.Services.AddSwaggerGen();


            // ثبت DbContext برای دسترسی به دیتابیس با اتصال به SQL Server
            // استفاده از رشته اتصال (Connection String) تعریف شده در appsettings.json تحت نام "DefaultConnection"
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ثبت Repositoryها با سطح Scoped (یک نمونه برای هر درخواست HTTP)
            // Repositoryها نقش دسترسی به داده‌ها و عملیات دیتابیس را ایفا می‌کنند
            builder.Services.AddScoped<IRequestTypeRepository, RequestTypeRepository>();
            builder.Services.AddScoped<IFundingSourceRepository, FundingSourceRepository>();
            builder.Services.AddScoped<IRequestingDepartmenRepository, RequestingDepartmenRepository>();
            builder.Services.AddScoped<IRequestRepository, RequestRepository>();
            builder.Services.AddScoped<IAllocationRepository, AllocationRepository>();
            builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

            // ثبت سرویس‌های Application که منطق کسب‌وکار برنامه را پیاده‌سازی می‌کنند
            builder.Services.AddScoped<IRequestTypeService, RequestTypeService>();
            builder.Services.AddScoped<IFundingSourceService, FundingSourceService>();
            builder.Services.AddScoped<IRequestingDepartmenService, RequestingDepartmenService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<IAllocationService, AllocationService>();
            builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            // ثبت Validator برای اعتبارسنجی داده‌ها هنگام ایجاد پرداخت‌ها (CreatePaymentDto)
            builder.Services.AddScoped<CreatePaymentDtoValidator>();
            builder.Services.AddScoped<UpdatePaymentDtoValidator>();
            builder.Services.AddScoped<UpdateAllocationDtoValidator>();

            // بارگذاری تنظیمات از فایل appsettings.json بصورت الزامی و با امکان reload خودکار در صورت تغییر فایل
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // ساخت نهایی برنامه پس از ثبت همه سرویس‌ها
            var app = builder.Build();

            // کانفیگ pipeline درخواست‌های HTTP

            // اگر محیط توسعه است (Development)، فعال‌سازی Swagger UI برای تست API در مرورگر
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(x=>x.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None));
            }

            // فعال‌سازی ریدایرکت خودکار از HTTP به HTTPS برای امنیت بیشتر
            app.UseHttpsRedirection();

            // فعال‌سازی Authorization (سطح دسترسی) روی درخواست‌ها
            app.UseAuthorization();

            // نگاشت کنترلرها به مسیرهای URL برای دریافت درخواست‌ها
            app.MapControllers();

            // شروع اجرای وب اپلیکیشن و گوش دادن به درخواست‌های ورودی
            app.Run();
        }
    }
}
