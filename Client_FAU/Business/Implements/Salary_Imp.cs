using Client_FAU.Business.Interfaces;
using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client_FAU.Business.Implements
{
    public class Salary_Imp : Salary_Int
    {
        private static string? baseAPIUrl;
        private readonly HttpClient _httpClient;
        private readonly string name = "salaries";
        private readonly IJSRuntime _jSRunTime;
        public Salary_Imp(HttpClient httpClient, IConfiguration configuration, IJSRuntime jSRunTime)
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

        public async Task<Salary> AddANewSalary(Salary salary)
        {
            var json = JsonConvert.SerializeObject(salary);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.PostAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Salary>(apiResponse)!;
        }

        public async Task<Salary> DeleteAnExistSalary(string salaryCode)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.DeleteAsync($"{baseAPIUrl}/{name}/{salaryCode}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Salary>(apiResponse)!;
        }

        public async Task<Salary> EditAnExistSalary(Salary salary)
        {
            var json = JsonConvert.SerializeObject(salary);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.PutAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Salary>(apiResponse)!;
        }

        public async Task<List<Salary>> GetSalaryList(int limit)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}?limit={limit}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Salary>>(apiResponse)!;
        }

        public async Task<Salary> GetTheSalaryBySalaryCode(string salaryCode)
        {
            await SetAuthorizationHeaderAsync();
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{salaryCode}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Salary>(apiResponse)!;
        }
    }
}
