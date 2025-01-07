using AutomationFramwork.API.Core.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramwork.API.Framework.ApiClients
{
    public interface IUserApiClient
    {
        Task<User> GetUserAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }

    public class UserApiClient : BaseApiClient, IUserApiClient
    {
        public UserApiClient(string baseUrl) : base(baseUrl) { }

        public async Task<User> GetUserAsync(int id)
        {
            var request = new RestRequest($"/users/{id}", Method.Get);
            return await SendRequestAsync<User>(request);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var request = new RestRequest("/users", Method.Post);
            request.AddJsonBody(user);
            return await SendRequestAsync<User>(request);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var request = new RestRequest($"/users/{user.Id}", Method.Put);
            request.AddJsonBody(user);
            return await SendRequestAsync<User>(request);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var request = new RestRequest($"/users/{id}", Method.Delete);
            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }
    }
}
