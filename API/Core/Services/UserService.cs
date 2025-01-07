using AutomationFramwork.API.Core.Interfaces;
using AutomationFramwork.API.Core.Models;
using AutomationFramwork.API.Framework.ApiClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramwork.API.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserApiClient _userApiClient;

        public UserService(IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _userApiClient.GetUserAsync(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _userApiClient.CreateUserAsync(user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _userApiClient.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userApiClient.DeleteUserAsync(id);
        }
    }
}
