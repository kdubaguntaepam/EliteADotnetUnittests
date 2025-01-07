using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramwork.API.Framework.Utilities
{
    public static class HttpRequestHelper
    {
        public static RestRequest CreateRequest(string endpoint, Method method)
        {
            return new RestRequest(endpoint, method);
        }
    }
}
