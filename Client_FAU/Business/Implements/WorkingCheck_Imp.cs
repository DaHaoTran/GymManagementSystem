using Client_FAU.Business.Interfaces;
using Models;
using Newtonsoft.Json;
using System.Text;

namespace Client_FAU.Business.Implements
{
    public class WorkingCheck_Imp : WorkingCheck_Int
    {
        private static string? baseAPIUrl;
        private readonly HttpClient _httpClient;
        private readonly string name = "working-checks";
        public WorkingCheck_Imp(IConfiguration configuration, HttpClient httpClient)
        {
            var apiUrl = configuration["BaseAPIUrl"];
            baseAPIUrl = apiUrl != null ? apiUrl : string.Empty;
            _httpClient = httpClient;
        }

        public async Task<WorkingCheck> AddANewWorkingCheck(WorkingCheck workCheck)
        {
            var json = JsonConvert.SerializeObject(workCheck);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var apiRequest = await _httpClient.PostAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WorkingCheck>(apiResponse)!;
        }

        public async Task<WorkingCheck> DeleteAnExistWorkingCheck(int orderNumber)
        {
            var apiRequest = await _httpClient.DeleteAsync($"{baseAPIUrl}/{name}/{orderNumber}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WorkingCheck>(apiResponse)!;
        }

        public async Task<WorkingCheck> EditAnExistWorkingCheck(WorkingCheck workCheck)
        {
            var json = JsonConvert.SerializeObject(workCheck);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var apiRequest = await _httpClient.PutAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WorkingCheck>(apiResponse)!;
        }

        public async Task<WorkingCheck> GetTheWorkingCheckByOrderNumber(int orderNumber)
        {
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{orderNumber}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WorkingCheck>(apiResponse)!;
        }

        public async Task<List<WorkingCheck>> GetTheWorkingChecksByAccountCode(string accountCode, string sort, int limit)
        {
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{accountCode}/accounts?sort={sort}&limit={limit}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<WorkingCheck>>(apiResponse)!; 
        }

        public async Task<List<WorkingCheck>> GetWorkingCheckList(string sort, int limit)
        {
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}?sort={sort}&limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<WorkingCheck>>(apiResponse)!;
        }
    }
}
