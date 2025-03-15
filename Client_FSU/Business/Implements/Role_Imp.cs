using Client_FSU.Business.Interfaces;
using Client_FSU.Variables;
using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client_FSU.Business.Implements
{
    public class Role_Imp : Role_Int
    {
        private static string? baseAPIUrl;
        private readonly HttpClient _httpClient;
        private readonly string name = "roles";
        public Role_Imp(IConfiguration configuration, HttpClient httpClient)
        {
            var apiUrl = configuration["BaseAPIUrl"];
            baseAPIUrl = apiUrl != null ? apiUrl : string.Empty;
            _httpClient = httpClient;
        }

        private void SetAuthorizationHeaderAsync()
        {
            if (string.IsNullOrEmpty(Validation.Token)) { return; }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Validation.Token);
        }

        public async Task<Role> AddANewRole(Role role)
        {
            var json = JsonConvert.SerializeObject(role);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.PostAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Role>(apiResponse)!;
        }

        public async Task<Role> DeleteAnExistRole(int orderNumber)
        {
            SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.DeleteAsync($"{baseAPIUrl}/{name}/{orderNumber}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Role>(apiResponse)!;
        }

        public async Task<Role> EditAnExistRole(Role role)
        {
            var json = JsonConvert.SerializeObject(role);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.PutAsync($"{baseAPIUrl}/{name}", stringContent);
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Role>(apiResponse)!;
        }

        public async Task<List<Role>> GetRoleList(int limit)
        {
            SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}?limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Role>>(apiResponse)!;
        }

        public async Task<Role> GetTheRoleByOrderNumber(int orderNumber)
        {
            SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{orderNumber}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Role>(apiResponse)!;
        }

        public async Task<List<Role>> GetTheRolesBySearchString(string str, int limit)
        {
            SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/filter?str={str}&limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Role>>(apiResponse)!;
        }
    }
}
