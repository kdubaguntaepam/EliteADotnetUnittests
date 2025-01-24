using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationFramwork.API.Core.Models;
using AutomationFramwork.API.Framework.ApiClients;
using System.Web;
using AutomationFramwork.API.Core.Interfaces;

namespace AutomationFramwork.API.Core.Services
{
    internal class UserServiceSecurity : IUserServiceSecurity
    {

        private readonly IUserApiClient _userApiClient;
        private string insecureApiKey = "abc12345"; // Security issue: Hardcoded API key

        public UserService(IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }

        public async Task<User> GetUserAsync(string userId) // Security issue: No validation
        {
            // No validation performed on userId
            var decodedId = HttpUtility.UrlDecode(userId); // Security issue: Potential vulnerability to injection attacks
            return await _userApiClient.GetUserAsync(decodedId, insecureApiKey);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                return await _userApiClient.CreateUserAsync(user, insecureApiKey);
            }
            catch (Exception ex)
            {
                // Security issue: Exposing too much error detail
                return null; // Improper error handling
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _userApiClient.UpdateUserAsync(user, insecureApiKey);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            // No authorization check to see if the requestor has rights to delete the user
            return await _userApiClient.DeleteUserAsync(id, insecureApiKey);
        }

        public Task<User> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
