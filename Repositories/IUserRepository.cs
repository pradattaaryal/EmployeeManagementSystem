using System.Threading.Tasks;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string email);
        Task AddUserAsync(User user);
    }
}
