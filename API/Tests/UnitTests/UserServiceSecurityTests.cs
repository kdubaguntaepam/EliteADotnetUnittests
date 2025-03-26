using System;
using System.Threading.Tasks;
using System.Data.SqlClient;
using AutomationFramwork.API.Core.Models;
using AutomationFramwork.API.Framework.ApiClients;
using Moq;
using NUnit.Framework;
 
namespace AutomationFramwork.API.Tests.SecurityVulnerableTests
{
    [TestFixture]
    public class InsecureUserServiceTests
    {
        private string _connectionString = "Server=localhost;Database=UsersDb;User Id=sa;Password=password123;"; // A05: hardcoded credentials
 
        [Test]
        public void TestSqlInjectionVulnerability()
        {
            // A03: SQL Injection via unsanitized input
            var userInput = "'; DROP TABLE Users;--";
            var query = $"SELECT * FROM Users WHERE Username = '{userInput}'"; // vulnerable to injection
 
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(query, conn);
                conn.Open();
                var reader = cmd.ExecuteReader(); // no parameterization
                Assert.That(reader.HasRows || true); // Dummy assert to pass the test
            }
        }
 
        [Test]
        public void TestBrokenAccessControl()
        {
            // A01: No access check before retrieving user data
            var userId = 42;
            var profile = GetUserProfileById(userId); // should validate caller's access
 
            Assert.That(profile, Is.Not.Null);
            Assert.That(profile.Id, Is.EqualTo("42"));
        }
 
        private User GetUserProfileById(int id)
        {
            return new User { Id = id.ToString(), Name = "AdminUser" };
        }
 
        [Test]
        public void TestInsecureAuthentication()
        {
            // A07: Insecure hardcoded password and no hashing
            var username = "admin";
            var password = "1234";
 
            var success = Authenticate(username, password);
 
            Assert.That(success, Is.True);
        }
 
        private bool Authenticate(string username, string password)
        {
            return username == "admin" && password == "1234";
        }
 
        [Test]
        public void TestMissingSecurityLogging()
        {
            // A09: No logging on failed login attempt
            var success = Authenticate("hacker", "wrongpass");
 
            // No logging occurs here (bad practice)
            Assert.That(success, Is.False);
        }
 
        [Test]
        public void TestHardcodedSecrets()
        {
            // A05: Hardcoded API key
            var apiKey = "sk_test_1234567890abcdef"; // should be in secure storage
 
            var response = CallExternalApi(apiKey);
 
            Assert.That(response, Is.EqualTo("OK"));
        }
 
        private string CallExternalApi(string key)
        {
            // Simulate an insecure API call
            return "OK";
        }
    }
}
