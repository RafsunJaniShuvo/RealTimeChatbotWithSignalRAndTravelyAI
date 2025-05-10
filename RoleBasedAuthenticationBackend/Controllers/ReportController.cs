using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuthenticationBackend.Repositories.Interfaces;

namespace RoleBasedAuthenticationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger<ReportController> _logger;
        public ReportController(IReportService reportService, ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        [HttpGet("CurrentStock")]
        public async Task<IActionResult> GetCurrentStock()
        {
            try
            {
                var result = await _reportService.GetCurrentStockAsync();
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Failed to generate current stock report");
                return StatusCode(500, "Unable to generate current stock report. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error generating current stock report");
                return StatusCode(500, "An unexpected error occurred while generating stock report.");
            }
        }

        [HttpGet("DateWiseStock")]
        public async Task<IActionResult> GetDateWiseStock([FromQuery] DateTime? fromDate,[FromQuery] DateTime? toDate)
        {
            try
            {
                if (fromDate > toDate)
                {
                    return BadRequest("fromDate cannot be after toDate.");
                }

                if (fromDate > DateTime.Today || toDate > DateTime.Today)
                {
                    return BadRequest("Date range cannot be in the future.");
                }

                var result = await _reportService.GetDateWiseStockReportAsync(fromDate, toDate);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid date parameters: {FromDate} to {ToDate}", fromDate, toDate);
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Failed to generate date-wise stock report");
                return StatusCode(500, "Unable to generate date-wise stock report. Please verify your date range.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error generating date-wise stock report");
                return StatusCode(500, "An unexpected error occurred while generating the report.");
            }
        }
    }
}
