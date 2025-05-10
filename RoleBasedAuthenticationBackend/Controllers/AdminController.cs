using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuthenticationBackend.Data;
using RoleBasedAuthenticationBackend.Models;

namespace RoleBasedAuthenticationBackend.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _DbContext;
        public AdminController(AppDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("You have accessed the admin controller");
        }

        [HttpGet("GetEmployee")]
        public IActionResult GetEmployee()
        {
            IQueryable<Employee> employees = _DbContext.Employees.Take(1);
            
            
            return Ok(employees);
        }
    }
}
