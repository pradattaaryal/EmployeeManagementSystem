using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repositories
{
    public class RestaurantRepository : IGenericRepository<Restaurant>
    {
        private readonly ApplicationDbContext _context;

        public RestaurantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants.Include(r => r.Menus).ToListAsync();
        }

        public async Task<Restaurant> GetByIdAsync(int id)
        {
            return await _context.Restaurants.Include(r => r.Menus).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Restaurant entity)
        {
            await _context.Restaurants.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Restaurant entity)
        {
            _context.Restaurants.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}
