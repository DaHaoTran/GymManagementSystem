using Client_FAU.Business.Interfaces;
using Client_FAU.Components.Pages;
using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Client_FAU.Business.Implements
{
    public class Branch_Imp : Branch_Int
    {
        private static string? baseAPIUrl;
        private readonly HttpClient _httpClient;
        private readonly string name = "branches";
        private readonly IJSRuntime _jSRunTime;
        public Branch_Imp(IConfiguration configuration, HttpClient httpClient, IJSRuntime jSRuntime)
        {
            var apiUrl = configuration["BaseAPIUrl"];
            baseAPIUrl = apiUrl != null ? apiUrl : string.Empty;
            _httpClient = httpClient;
            _jSRunTime = jSRuntime;
        }

        private async Task SetAuthorizationHeaderAsync()
        {
            var token = await _jSRunTime.InvokeAsync<string>("localStorage.getItem", "jwtToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<Branch> AddNewBranch(Branch branch)
        {
            var json = JsonConvert.SerializeObject(branch);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.PostAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Branch>(apiResponse)!;
        }

        public async Task<Branch> DeleteAnExistBranch(string branchCode)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.DeleteAsync($"{baseAPIUrl}/{name}/{branchCode}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Branch>(apiResponse)!;
        }

        public async Task<Branch> EditAnExistBranch(Branch branch)
        {
            var json = JsonConvert.SerializeObject(branch);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.PutAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Branch>(apiResponse)!;
        }

        public async Task<List<Branch>> GetBranchList(int limit)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}?limit={limit}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Branch>>(apiResponse)!;
        }

        public async Task<Branch> GetTheBranchByBranchCode(string branchCode)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{branchCode}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Branch>(apiResponse)!;
        }

        public async Task<List<Branch>> GetTheBranchesBySearchString(string str, int limit)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/filter?str={str}&limit={limit}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Branch>>(apiResponse)!;
        }
    }
}
