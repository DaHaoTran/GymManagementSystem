using Client_FAU1.Business.Interfaces;
using Models;
using Newtonsoft.Json;
using System.Text;

namespace Client_FAU1.Business.Implements
{
    public class Salary_Imp : Salary_Int
    {
        private static string? baseAPIUrl;
        private readonly HttpClient _httpClient;
        private readonly string name = "salaries";
        public Salary_Imp(HttpClient httpClient, IConfiguration configuration)
        {
            var apiUrl = configuration["BaseAPIUrl"];
            baseAPIUrl = apiUrl != null ? apiUrl : string.Empty;
            _httpClient = httpClient;
        }

        public async Task<Salary> AddANewSalary(Salary salary)
        {
            var json = JsonConvert.SerializeObject(salary);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var apiRequest = await _httpClient.PostAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Salary>(apiResponse)!;
        }

        public async Task<Salary> DeleteAnExistSalary(string salaryCode)
        {
            var apiRequest = await _httpClient.DeleteAsync($"{baseAPIUrl}/{name}/{salaryCode}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Salary>(apiResponse)!;
        }

        public async Task<Salary> EditAnExistSalary(Salary salary)
        {
            var json = JsonConvert.SerializeObject(salary);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var apiRequest = await _httpClient.PutAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Salary>(apiResponse)!;
        }

        public async Task<List<Salary>> GetSalaryList(int limit)
        {
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}?limit={limit}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Salary>>(apiResponse)!;
        }

        public async Task<Salary> GetTheSalaryBySalaryCode(string salaryCode)
        {
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{salaryCode}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Salary>(apiResponse)!;
        }
    }
}
