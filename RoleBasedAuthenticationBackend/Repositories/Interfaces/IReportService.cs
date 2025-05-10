using RoleBasedAuthenticationBackend.DTOs;

namespace RoleBasedAuthenticationBackend.Repositories.Interfaces
{
    public interface IReportService
    {
        Task<IList<CurrentStockReportDto>> GetCurrentStockAsync();

        Task<IList<DateWiseStockReportDto>> GetDateWiseStockReportAsync(DateTime? fromDate, DateTime? toDate);
    }
}
