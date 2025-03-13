using Client_FAU.Business.Interfaces;
using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Client_FAU.Business.Implements
{
    public class Fine_Imp : Fine_Int
    {
        private static string? baseAPIUrl;
        private readonly HttpClient _httpClient;
        private readonly string name = "fines";
        private readonly IJSRuntime _jSRunTime;
        public Fine_Imp(IConfiguration configuration, HttpClient httpClient, IJSRuntime jSRunTime)
        {
            var apiUrl = configuration["BaseAPIUrl"];
            baseAPIUrl = apiUrl != null ? apiUrl : string.Empty;
            _httpClient = httpClient;
            _jSRunTime = jSRunTime;
        }

        private async Task SetAuthorizationHeaderAsync()
        {
            var token = await _jSRunTime.InvokeAsync<string>("localStorage.getItem", "jwtToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<Fine> AddANewFine(Fine fine)
        {
            var json = JsonConvert.SerializeObject(fine);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.PostAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Fine>(apiResponse)!;
        }

        public async Task<Fine> DeleteAnExistFine(Guid fineCode)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.DeleteAsync($"{baseAPIUrl}/{name}/{fineCode}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Fine>(apiResponse)!;
        }

        public async Task<Fine> EditAnExistFine(Fine fine)
        {
            var json = JsonConvert.SerializeObject(fine);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.PutAsync($"{baseAPIUrl}/{name}", stringContent);
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Fine>(apiResponse)!;
        }

        public async Task<List<Fine>> GetFineList(string sort, int limit)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}?sort={sort}&limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiReponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Fine>>(apiReponse)!;
        }

        public async Task<Fine> GetTheFineByFineCode(Guid fineCode)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{fineCode}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Fine>(apiResponse)!;
        }

        public async Task<List<Fine>> GetTheFinesByCustomerCode(string customerCode, string sort, int limit)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{customerCode}/customers?sort={sort}&limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiReponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Fine>>(apiReponse)!;
        }
    }
}
