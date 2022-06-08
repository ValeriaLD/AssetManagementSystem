using AssetManagementSystem.Server.Data;
using AssetManagementSystem.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;

        public EmployeeController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            var employees = await _appDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = _appDbContext.Employees
                .FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound("The employee not found!");
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> CreateEmployee(Employee employee)
        {
            _appDbContext.Employees.Add(employee);
            await _appDbContext.SaveChangesAsync();

            return Ok(await GetDbEmployees());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee employee, int id)
        {
            var dbEmployee = await _appDbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (dbEmployee == null)
                return NotFound("Employee not found!");

            dbEmployee.IDNP = employee.IDNP;
            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.LastName = employee.LastName;
            dbEmployee.Email = employee.Email;
            dbEmployee.PersonalPhone = employee.PersonalPhone;

            await _appDbContext.SaveChangesAsync();

            return Ok(await GetDbEmployees());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var dbEmployee = await _appDbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (dbEmployee == null)
                return NotFound("Employee not found!");

            _appDbContext.Employees.Remove(dbEmployee);
            await _appDbContext.SaveChangesAsync();

            return Ok(await GetDbEmployees());
        }

        private async Task<List<Employee>> GetDbEmployees()
        {
            return await _appDbContext.Employees.ToListAsync();
        }
    }
}
