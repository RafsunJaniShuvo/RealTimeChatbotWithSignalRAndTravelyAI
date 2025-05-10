using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RoleBasedAuthenticationBackend.Data;
using RoleBasedAuthenticationBackend.DTOs;
using RoleBasedAuthenticationBackend.Repositories.Interfaces;
using System.Data;
using System.Data.Common;

namespace RoleBasedAuthenticationBackend.Repositories.Implementations
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _appDbContext;
        public ReportRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IList<CurrentStockReportDto>> GetCurrentStockReportAsync()
        {
            try
            {
                return await _appDbContext.Products
                    .Where(p => !p.IsDeleted)
                    .Select(p => new CurrentStockReportDto
                    {
                        ProductName = p.Name,
                        SKU = p.SKU,
                        Price = p.Price,
                        CurrentStockQty = p.StockQty
                    })
                    .ToListAsync();
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException("Failed to generate current stock report due to database error. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while generating current stock report.", ex);
            }
        }

        public async Task<IList<DateWiseStockReportDto>> GetDateWiseStockReportAsync(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var fromParam = new SqlParameter("@FromDate", SqlDbType.DateTime)
                {
                    Value = fromDate ?? (object)DBNull.Value
                };

                var toParam = new SqlParameter("@ToDate", SqlDbType.DateTime)
                {
                    Value = toDate ?? (object)DBNull.Value
                };


                if (fromDate > toDate)
                {
                    throw new ArgumentException("Invalid date range specified. 'From' date must be before 'To' date.");
                }

                var result = await _appDbContext.Set<DateWiseStockReportDto>()
                   .FromSqlRaw("EXEC [dbo].[sp_GetDateWiseStockReport] @FromDate, @ToDate", fromParam, toParam)
                   .ToListAsync();


                return result;

            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (DbException ex)
            {
                throw new InvalidOperationException("Failed to generate date-wise stock report due to database error. Please verify your date range and try again.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while generating date-wise stock report.", ex);
            }
        }



    }
}
