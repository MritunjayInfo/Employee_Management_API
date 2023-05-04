using Employee_Management_API.DataContext;
using Employee_Management_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var employees = await _employeeDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Create(Employee employee)
        {
            
                // Add the employee to the context
                _employeeDbContext.Employees.Add(employee);

                // Save the changes to the database
                await _employeeDbContext.SaveChangesAsync();

                return Ok(employee);           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeDbContext.Set<Employee>().FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employeeDbContext.Set<Employee>().Remove(employee);
            await _employeeDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Employee updatedEmployee)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            // Update the properties of the existing employee with the values of the updated employee
            employee.First_Name = updatedEmployee.First_Name;
            employee.Last_Name = updatedEmployee.Last_Name;
            employee.Email_Id = updatedEmployee.Email_Id;
            employee.Mobile_Number = updatedEmployee.Mobile_Number;
            employee.Address = updatedEmployee.Address;
            employee.Zip_Code = updatedEmployee.Zip_Code;

            _employeeDbContext.Entry(employee).State = EntityState.Modified;

            try
            {
                await _employeeDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _employeeDbContext.Employees.Any(e => e.Id == id);
        }

    }
}
