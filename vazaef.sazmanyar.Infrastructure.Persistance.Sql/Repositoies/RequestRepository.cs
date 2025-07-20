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

        public RequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RequestEntity>> GetAllAsync() =>
            await _context.Requests
                .Include(r => r.ActionBudgetRequests)
                .ToListAsync();

        public async Task<RequestEntity> GetByIdAsync(long id) =>
    await _context.Requests
        .Include(r => r.ActionBudgetRequests) // ✅ این خط اضافه بشه
        .FirstOrDefaultAsync(r => r.Id == id);

        public async Task AddAsync(RequestEntity request)
        {
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RequestEntity request)
        {
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<string> GetAllRequestsWithTotalBudgetJsonAsync()
        {
            using var connection = _context.Database.GetDbConnection();
            var result = await connection.QueryAsync(
                sql: "sp_GetAllRequestsWithTotalBudget",
                commandType: CommandType.StoredProcedure);

            var json = JsonSerializer.Serialize(result);
            return json;
        }
        public async Task<string> GetRequestsByIdsWithTotalBudgetJsonAsync(IEnumerable<long> ids)
        {
            var idList = string.Join(",", ids); // مثل: "1,2,3"

            var parameters = new DynamicParameters();
            parameters.Add("@Ids", idList);

            using var connection = _context.Database.GetDbConnection();

            // اگر اتصال باز نیست، باز کن
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

    }
}
