using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using RoleBasedAuthenticationBackend.Data;
using RoleBasedAuthenticationBackend.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RoleBasedAuthenticationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _DbContext;
        public EmployeeController(AppDbContext DbContext) {
            _DbContext = DbContext;
        }


        [HttpGet("GetEmployee")]

        public IActionResult GetEmployee( ) {

            IList<Employee> employee = _DbContext.Employees.ToList();
            return Ok(employee);
        }

        [HttpPost("CreateEmployee")]
        public IActionResult CreateEmp(EmployeeCreateDTO emp)
        {
            Employee employee = new Employee()
            {
                Name = emp.Name,
                PhoneNumber = emp.PhoneNumber,
                Position = emp.Position,
                Company = emp.Company,
            };
            _DbContext.Employees.Add(employee);
            _DbContext.SaveChanges();
            return Ok();
        }

        [HttpPost("UpdateEmployee")]
        public IActionResult  UpadateEmp(int id, EmployeeUpdateDTO emp) { 

            var employee =  _DbContext.Employees.Find(id);
            if (employee == null) { 
                return NotFound();
            }
            employee.Name = emp.Name;
            employee.PhoneNumber = emp.PhoneNumber; 
            employee.Position = emp.Position;
            employee.Company = emp.Company;
            _DbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete("EmployeeDelete")]
        public IActionResult DeleteEmp(int id) {

            var employee = _DbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            _DbContext.Employees.Remove(employee);
            _DbContext.SaveChanges();
            return Ok("Succesfully deleted");

        }
    }
}
