using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IGenericRepository<Menu> _menuRepository;

        public MenuController(IGenericRepository<Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            var menus = await _menuRepository.GetAllAsync();
            return Ok(menus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var menu = await _menuRepository.GetByIdAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }

        [HttpPost]
        public async Task<ActionResult> AddMenu(Menu menu)
        {
            await _menuRepository.AddAsync(menu);
            return CreatedAtAction(nameof(GetMenu), new { id = menu.Id }, menu);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMenu(int id, Menu menu)
        {
            if (id != menu.Id)
            {
                return BadRequest();
            }
            await _menuRepository.UpdateAsync(menu);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMenu(int id)
        {
            await _menuRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
