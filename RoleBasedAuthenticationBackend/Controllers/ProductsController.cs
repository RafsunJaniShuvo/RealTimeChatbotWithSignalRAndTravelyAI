using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuthenticationBackend.DTOs;
using RoleBasedAuthenticationBackend.Repositories.Interfaces;

namespace RoleBasedAuthenticationBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService service, ILogger<ProductsController> logger)
        {
            _service = service;
            _logger = logger;   
        }


        [HttpGet("GetProductsForSale")]
        public async Task<IActionResult> GetAll()
        {
            try
            { 
                var result = await _service.GetAllAsync();
                return Ok(result);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch products");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAll(int draw = 0,int page = 1,int pageSize = 5,string? search = "")
        {
            var (products, totalCount) = await _service.GetAllAsync(search, page, pageSize);
            return new JsonResult(new
            {
                draw = draw,
                recordsTotal = totalCount,
                recordsFiltered = totalCount,
                data = products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.CreateAsync(dto);
             return Ok(new { message = "Product created successfully" });
        }

        [HttpPut("UpdateById/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.UpdateAsync(id, dto);
            return Ok(new { message = "Product updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new { message = "Product deleted (soft) successfully" });
        }
    }
}
