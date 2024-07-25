using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repositories
{
    public class MenuRepository : IGenericRepository<Menu>
    {
        private readonly ApplicationDbContext _context;

        public MenuRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await _context.Menus.Include(m => m.Restaurant).ToListAsync();
        }

        public async Task<Menu> GetByIdAsync(int id)
        {
            return await _context.Menus.Include(m => m.Restaurant).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Menu entity)
        {
            await _context.Menus.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Menu entity)
        {
            _context.Menus.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
        }
    }
}
