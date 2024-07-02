using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using EmployeeManagementSystem.Helpers;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (await _repository.GetUserAsync(user.Email) != null)
            {
                return BadRequest("User already exists.");
            }

            await _repository.AddUserAsync(user);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            var existingUser = await _repository.GetUserAsync(user.Email);
            if (existingUser == null || existingUser.Password != user.Password)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(RandomStringGenerator.GenerateRandomBase64String()); // Same secret key as in Program.cs
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, existingUser.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time (adjust as needed)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Return token as response
            return Ok(new { Token = tokenString });
        }
    }
}
