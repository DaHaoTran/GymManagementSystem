using Client_FAU.Business.Interfaces;
using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace Client_FAU.Business.Implements
{
    public class Jwt_Imp : Jwt_Int
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRunTime;
        public Jwt_Imp(HttpClient httpClient, IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _jsRunTime = jSRuntime;
        }
        public async Task SetAuthorizationHeaderAsync(string localSName)
        {
            var token = await _jsRunTime.InvokeAsync<string>("localStorage.getItem", localSName);
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
