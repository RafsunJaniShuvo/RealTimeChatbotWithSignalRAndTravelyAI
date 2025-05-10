using RoleBasedAuthenticationBackend.DTOs;

namespace RoleBasedAuthenticationBackend.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<IList<CurrentStockReportDto>> GetCurrentStockReportAsync();
        Task<IList<DateWiseStockReportDto>> GetDateWiseStockReportAsync(DateTime? fromDate, DateTime? toDate);
    }
}
