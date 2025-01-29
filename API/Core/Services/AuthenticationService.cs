using System.Security.Cryptography;
using System.Text;
using System.Web;
using AutomationFramwork.API.Core.Interfaces;
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

            return await _userApiClient.GetUserAsync(Convert.ToInt32(decodedId)); 

        }
 
        // Exposes sensitive information via error

        public async Task<User> CreateUserAsync(User user)

        {

            try

            {

                return await _userApiClient.CreateUserAsync(user);

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

            return await _userApiClient.UpdateUserAsync(user);

        }
 
        // No authorization check for deletion of the user

        public async Task<bool> DeleteUserAsync(int id)

        {

            return await _userApiClient.DeleteUserAsync(id);

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
    // Insecure cryptographic storage: Storing sensitive data like credit card numbers without encryption
    public class SensitiveDataService
    {
        // Vulnerability: Storing sensitive data (credit card) as plaintext
        public void StoreCreditCardDetails(string creditCardNumber)
        {
            Console.WriteLine($"Storing credit card: {creditCardNumber}"); // Plaintext storage of sensitive information
        }

        // Weak encryption algorithm used
        public string EncryptCreditCard(string creditCardNumber)
        {
            // Security issue: Using MD5 (weak) for encryption
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(creditCardNumber);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }

    public class DocumentService : IDocumentService
    {
        // Insecure Direct Object References (IDOR): User can access any document with an ID
        public string GetDocument(string documentId)
        {
            // Vulnerability: No access control or validation to verify if the user should access the document
            return $"Document {documentId} content.";
        }
    }

}


 





