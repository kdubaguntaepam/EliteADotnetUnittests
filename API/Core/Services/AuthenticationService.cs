using System;

using System.Threading.Tasks;

using System.Web;

using AutomationFramwork.API.Core.Models;

using AutomationFramwork.API.Framework.ApiClients;
 
namespace AutomationFramwork.API.Core.Interfaces

{

    public interface IUserServiceSecurity

    {

        Task<User> GetUserAsync(string userId);

        Task<User> CreateUserAsync(User user);

        Task<User> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(int id);

    }
 
    public interface IAuthenticationService

    {

        bool Login(string username, string password);

    }
 
    public interface IDocumentService

    {

        string GetDocument(string documentId);

    }

}
 
namespace AutomationFramwork.API.Core.Services

{

    internal class UserServiceSecurity : IUserServiceSecurity

    {

        private readonly IUserApiClient _userApiClient;

        private string insecureApiKey = "abc12345"; // Security issue: Hardcoded API key
 
        public UserServiceSecurity(IUserApiClient userApiClient)

        {

            _userApiClient = userApiClient;

        }
 
        // SQL Injection Vulnerability: User input is directly used in SQL query

        public async Task<User> GetUserAsync(string userId)

        {

            var decodedId = HttpUtility.UrlDecode(userId); // Potential XSS vulnerability if decodedId is not properly sanitized

            return await _userApiClient.GetUserAsync(decodedId, insecureApiKey); 

        }
 
        // Exposes sensitive information via error

        public async Task<User> CreateUserAsync(User user)

        {

            try

            {

                return await _userApiClient.CreateUserAsync(user, insecureApiKey);

            }

            catch (Exception ex)

            {

                // Security issue: Exposing detailed error messages

                Console.WriteLine(ex.Message); // Log sensitive exception details

                return null;

            }

        }
 
        public async Task<User> UpdateUserAsync(User user)

        {

            return await _userApiClient.UpdateUserAsync(user, insecureApiKey);

        }
 
        // No authorization check for deletion of the user

        public async Task<bool> DeleteUserAsync(int id)

        {

            return await _userApiClient.DeleteUserAsync(id, insecureApiKey);

        }

    }
 
    public class AuthenticationService : IAuthenticationService

    {

        private readonly string apiKey = "123456789-secret-key"; // Security issue: Hardcoded sensitive data
 
        // Plaintext password comparison vulnerability

        public bool Login(string username, string password)

        {

            // Security issue: Comparing password directly with hardcoded apiKey

            return password == apiKey;

        }

    }

}

 


 
