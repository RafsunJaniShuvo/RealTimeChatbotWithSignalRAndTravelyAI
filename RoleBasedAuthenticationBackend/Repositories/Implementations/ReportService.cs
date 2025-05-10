using RoleBasedAuthenticationBackend.DTOs;
using RoleBasedAuthenticationBackend.Repositories.Interfaces;

namespace RoleBasedAuthenticationBackend.Repositories.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepo;
        public ReportService(IReportRepository reportRepo)
        {
            _reportRepo = reportRepo;
        }

        public Task<IList<CurrentStockReportDto>> GetCurrentStockAsync()
        {
            return _reportRepo.GetCurrentStockReportAsync();
        }

        public Task<IList<DateWiseStockReportDto>> GetDateWiseStockReportAsync(DateTime? fromDate, DateTime? toDate)
        {
            return _reportRepo.GetDateWiseStockReportAsync(fromDate, toDate);
        }
    }
}
