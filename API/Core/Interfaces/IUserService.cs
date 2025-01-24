using AutomationFramwork.API.Core.Models;

namespace AutomationFramwork.API.Core.Interfaces
{
    public interface IUserServiceSecurity
    {
        Task<User> GetUserAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
