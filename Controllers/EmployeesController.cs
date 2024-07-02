using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _repository;

        public EmployeesController(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employee = await _repository.Get(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            await _repository.Add(employee);
            return CreatedAtAction(nameof(Get), new { id = employee.EmployeeID }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            await _repository.Update(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _repository.Delete(id);
            if (employee == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
