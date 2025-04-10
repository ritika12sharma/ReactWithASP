using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWithAsp.Server.Data;
using ReactWithAsp.Server.Models;

namespace ReactWithAsp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext dbcontext;
        public EmployeeController(AppDbContext dbcontext) => this.dbcontext = dbcontext;



        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await dbcontext.Employees.ToListAsync());


        [HttpPost]
        public async Task<IActionResult> Create(Employees employee)
        {
            // Validation: Mandatory fields
            if (string.IsNullOrWhiteSpace(employee.Name) || employee.Salary <= 0)
                return BadRequest("Invalid input.");

            // BirthDate < JoiningDate check
            if (employee.JoiningDate <= employee.Birthdate)
                return BadRequest("Joining date must be after birth date.");

            // Unique employee check
            var exists = await dbcontext.Employees.AnyAsync(e =>
                e.Name == employee.Name &&
                e.Birthdate == employee.Birthdate &&
                e.JoiningDate == employee.JoiningDate);
            if (exists)
                return BadRequest("Employee already exists.");

            dbcontext.Employees.Add(employee);
            await dbcontext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await dbcontext.Employees.FindAsync(id);
            return emp == null ? NotFound() : Ok(emp);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employees emp)
        {
            if (id != emp.Id) return BadRequest();
            var existing = await dbcontext.Employees.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = emp.Name;
            existing.Salary = emp.Salary;
            existing.Birthdate = emp.Birthdate;
            existing.JoiningDate = emp.JoiningDate;

            await dbcontext.SaveChangesAsync();
            return Ok(existing);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var emp = await dbcontext.Employees.FindAsync(id);
            if (emp == null) return NotFound();
            dbcontext.Employees.Remove(emp);
            await dbcontext.SaveChangesAsync();
            return Ok();
        }




    }
}
