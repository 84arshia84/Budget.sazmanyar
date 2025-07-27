using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using vazaef.sazmanyar.Domain.Modles.Request;

namespace vazaef.sazmanyar.Infrastructure.Persistance.Sql.Repositoies
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _context;

        // دریافت context از DI
        public RequestRepository(AppDbContext context)
        {
            _context = context;
        }

        // گرفتن همه درخواست‌ها با بارگذاری اکشن‌های بودجه مربوط به هر درخواست
        public async Task<IEnumerable<RequestEntity>> GetAllAsync() =>
            await _context.Requests
                .Include(r => r.ActionBudgetRequests)
                .ToListAsync();

        // گرفتن یک درخواست خاص با شناسه، همراه با اکشن‌های بودجه مربوطه
        public async Task<RequestEntity> GetByIdAsync(long id) =>
            await _context.Requests
                .Include(r => r.ActionBudgetRequests) // بارگذاری داده‌های مرتبط
                .FirstOrDefaultAsync(r => r.Id == id);

        // افزودن یک درخواست جدید
        public async Task AddAsync(RequestEntity request)
        {
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        // به‌روزرسانی درخواست موجود (کامل)
        public async Task UpdateAsync(RequestEntity request)
        {
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();
        }

        // حذف درخواست با شناسه مشخص
        public async Task DeleteAsync(long id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
            }
        }

        // دریافت همه درخواست‌ها به همراه بودجه کل، به صورت JSON با استفاده از Stored Procedure و Dapper
        public async Task<string> GetAllRequestsWithTotalBudgetJsonAsync()
        {
            using var connection = _context.Database.GetDbConnection();
            var result = await connection.QueryAsync(
                sql: "sp_GetAllRequestsWithTotalBudget",
                commandType: CommandType.StoredProcedure);

            var json = JsonSerializer.Serialize(result);
            return json;
        }

        // گرفتن درخواست‌ها بر اساس مجموعه‌ای از شناسه‌ها، به همراه بودجه کل، به صورت JSON
        public async Task<string> GetRequestsByIdsWithTotalBudgetJsonAsync(IEnumerable<long> ids)
        {
            var idList = string.Join(",", ids); // ایجاد رشته‌ای مانند "1,2,3"

            var parameters = new DynamicParameters();
            parameters.Add("@Ids", idList);

            using var connection = _context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            var result = await connection.QueryAsync(
                "sp_GetRequestsByIdsWithTotalBudget",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var json = JsonSerializer.Serialize(result);
            return json;
        }

        // گرفتن همه درخواست‌ها به همراه اکشن‌های بودجه (دوباره با Include)
        public async Task<List<RequestEntity>> GetAllWithActionBudgetRequestsAsync()
        {
            return await _context.Requests
                .Include(r => r.ActionBudgetRequests)
                .ToListAsync();
        }
    }
}
