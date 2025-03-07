using Client_FAU.Business.Interfaces;
using Models;
using Newtonsoft.Json;
using System.Text;

namespace Client_FAU.Business.Implements
{
    public class Equipment_Imp : Equipment_Int
    {
        private static string? baseAPIUrl;
        private readonly HttpClient _httpClient;
        private readonly string name = "equipment";
        public Equipment_Imp(IConfiguration configuration, HttpClient httpClient)
        {
            var apiUrl = configuration["BaseAPIUrl"];
            baseAPIUrl = apiUrl != null ? apiUrl : string.Empty;
            _httpClient = httpClient;
        }

        public async Task<Equipment> AddANewEquipment(Equipment equipment)
        {
            var json = JsonConvert.SerializeObject(equipment);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var apiRequest = await _httpClient.PostAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Equipment>(apiResponse)!;
        }

        public async Task<Equipment> DeleteAnExistEquipment(string equipCode)
        {
            var apiRequest = await _httpClient.DeleteAsync($"{baseAPIUrl}/{name}/{equipCode}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Equipment>(apiResponse)!;
        }

        public async Task<Equipment> EditAnExistEquipment(Equipment equipment)
        {
            var json = JsonConvert.SerializeObject(equipment);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var apiRequest = await _httpClient.PutAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) {return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Equipment>(apiResponse)!;
        }

        public async Task<List<Equipment>> GetEquipmentList(int limit)
        {
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}?limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Equipment>>(apiResponse)!;
        }

        public async Task<Equipment> GetTheEquipmentByEquipCode(string equipCode)
        {
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{equipCode}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Equipment>(apiResponse)!;  
        }

        public async Task<List<Equipment>> GetTheEquipmentBySearchString(string str, int limit)
        {
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/filter?str={str}&limit{limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Equipment>>(apiResponse)!;
        }
    }
}
