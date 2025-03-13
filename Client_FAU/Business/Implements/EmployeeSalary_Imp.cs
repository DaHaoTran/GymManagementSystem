using Client_FAU.Business.Interfaces;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Client_FAU.Business.Implements
{
    public class EmployeeSalary_Imp : EmployeeSalary_Int
    {
        private static string? baseAPIUrl;
        private readonly HttpClient _httpClient;
        private readonly string name = "employee-salaries";
        private readonly Jwt_Int _jwt;
        public EmployeeSalary_Imp(IConfiguration configuration, HttpClient httpClient, Jwt_Int jwt)
        {
            var apiUrl = configuration["BaseAPIUrl"];
            baseAPIUrl = apiUrl != null ? apiUrl : string.Empty;
            _httpClient = httpClient;
            _jwt = jwt;
        }

        public async Task<EmployeeSalary> AddANewEmployeeSalary(EmployeeSalary employeeSalary)
        {
            var json = JsonConvert.SerializeObject(employeeSalary);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.PostAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EmployeeSalary>(apiResponse)!;
        }

        public async Task<EmployeeSalary> DeleteAnExistEmployeeSalary(Guid empSalCode)
        {
            var apiRequest = await _httpClient.DeleteAsync($"{baseAPIUrl}/{name}/{empSalCode}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EmployeeSalary>(apiResponse)!;
        }

        public async Task<EmployeeSalary> EditAnExistEmployeeSalary(EmployeeSalary employeeSalary)
        {
            var json = JsonConvert.SerializeObject(employeeSalary);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.PutAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) {return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EmployeeSalary>(apiResponse)!;
        }

        public async Task<List<EmployeeSalary>> GetEmployeeSalaryList(string sort, int limit)
        {
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}?sort={sort}&limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<EmployeeSalary>>(apiResponse)!;
        }

        public async Task<List<EmployeeSalary>> GetTheEmployeeSalariesByAccountCode(string accountCode, string sort, int limit)
        {
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{accountCode}/accounts?sort={sort}&limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<EmployeeSalary>>(apiResponse)!;
        }

        public async Task<List<EmployeeSalary>> GetTheEmployeeSalariesByMonth(int month, int year)
        {
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/filter2?month={month}&year={year}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<EmployeeSalary>>(apiResponse)!;
        }

        public async Task<List<EmployeeSalary>> GetTheEmployeeSalariesBySearchString(string str, int limit)
        {
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/filter?str={str}&limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<EmployeeSalary>>(apiResponse)!;
        }

        public async Task<EmployeeSalary> GetTheEmployeeSalaryByEmployeeSalaryCode(Guid empSalCode)
        {
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{empSalCode}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EmployeeSalary>(apiResponse)!;
     
        }
    }
}
