using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuthenticationBackend.DTOs;
using RoleBasedAuthenticationBackend.Repositories.Interfaces;

namespace RoleBasedAuthenticationBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly ILogger<SalesController> _logger;
        public SalesController(ISaleService saleService, ILogger<SalesController> logger)
        {
            _saleService = saleService;
            _logger = logger;
        }

        [HttpGet("GetAllSales")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sales = await _saleService.GetAllAsync();
                return Ok(sales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales list");
                return StatusCode(500, "An error occurred while retrieving sales data. Please try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sale = await _saleService.GetByIdAsync(id);
                return sale == null ? NotFound($"Sale with ID {id} not found") : Ok(sale);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Sale not found with ID: {id}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving sale with ID {id}");
                return StatusCode(500, "An error occurred while retrieving the sale details.");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateSaleDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid sale creation request: {@ValidationErrors}", ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest(ModelState);
            }

            try
            {
                await _saleService.CreateAsync(dto);
                return Ok(new { message = "Sale recorded successfully" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid sale creation request");
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Sale creation failed due to business rules");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sale");
                return StatusCode(500, "An unexpected error occurred while recording the sale. Please try again.");
            }
        }
    }
}
