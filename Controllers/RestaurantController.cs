using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IGenericRepository<Restaurant> _restaurantRepository;

        public RestaurantController(IGenericRepository<Restaurant> restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<ActionResult> AddRestaurant(Restaurant restaurant)
        {
            await _restaurantRepository.AddAsync(restaurant);
            return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.Id }, restaurant);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }
            await _restaurantRepository.UpdateAsync(restaurant);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRestaurant(int id)
        {
            await _restaurantRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
