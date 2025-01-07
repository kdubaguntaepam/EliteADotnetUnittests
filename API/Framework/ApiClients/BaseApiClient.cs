using AutomationFramwork.API.Framework.Exceptions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramwork.API.Framework.ApiClients
{
    public abstract class BaseApiClient
    {
        protected readonly RestClient _client;

        protected BaseApiClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        protected async Task<T> SendRequestAsync<T>(RestRequest request) where T : new()
        {
            var response = await _client.ExecuteAsync<T>(request);
            if (!response.IsSuccessful)
            {
                throw new ApiException(response.StatusCode, response.Content);
            }
            return response.Data;
        }
    }
}
