using Client_FAU.Business.Interfaces;
using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client_FAU.Business.Implements
{
    public class Role_Imp : Role_Int
    {
        private static string? baseAPIUrl;
        private readonly HttpClient _httpClient;
        private readonly string name = "roles";
        private readonly IJSRuntime? _jSRunTime;
        public Role_Imp(IConfiguration configuration, HttpClient httpClient, IJSRuntime jSRunTime)
        {
            var apiUrl = configuration["BaseAPIUrl"];
            baseAPIUrl = apiUrl != null ? apiUrl : string.Empty;
            _httpClient = httpClient;
            _jSRunTime = jSRunTime;
        }

        private async Task SetAuthorizationHeaderAsync()
        {
            var token = await _jSRunTime!.InvokeAsync<string>("localStorage.getItem", "jwtToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<Role> AddANewRole(Role role)
        {
            var json = JsonConvert.SerializeObject(role);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.PostAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Role>(apiResponse)!;
        }

        public async Task<Role> DeleteAnExistRole(int orderNumber)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.DeleteAsync($"{baseAPIUrl}/{name}/{orderNumber}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Role>(apiResponse)!;
        }

        public async Task<Role> EditAnExistRole(Role role)
        {
            var json = JsonConvert.SerializeObject(role);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.PutAsync($"{baseAPIUrl}/{name}", stringContent);
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Role>(apiResponse)!;
        }

        public async Task<List<Role>> GetRoleList(int limit)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}?limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Role>>(apiResponse)!;
        }

        public async Task<Role> GetTheRoleByOrderNumber(int orderNumber)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{orderNumber}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Role>(apiResponse)!;
        }

        public async Task<List<Role>> GetTheRolesBySearchString(string str, int limit)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/filter?str={str}&limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Role>>(apiResponse)!;
        }
    }
}
