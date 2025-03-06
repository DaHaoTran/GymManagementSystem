using Client_FAU1.Business.Interfaces;
using Models;
using Newtonsoft.Json;
using System.Text;

namespace Client_FAU1.Business.Implements
{
    public class ServicePackage_Imp : ServicePackage_Int
    {
        private static string? baseAPIUrl;
        private readonly HttpClient _httpClient;
        private readonly string name = "service-packages";
        public ServicePackage_Imp(IConfiguration configuration, HttpClient httpClient)
        {
            var apiUrl = configuration["BaseAPIUrl"];
            baseAPIUrl = apiUrl != null ? apiUrl : string.Empty;
            _httpClient = httpClient;
        }
        
        public async Task<ServicePackage> AddANewServicePackage(ServicePackage servicePackage)
        {
            var json = JsonConvert.SerializeObject(servicePackage);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var apiRequest = await _httpClient.PostAsync($"{baseAPIUrl}/{name}", stringContent);
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServicePackage>(apiResponse)!;
        }

        public async Task<ServicePackage> DeleteAnExistServicePackage(string packageCode)
        {
            var apiRequest = await _httpClient.DeleteAsync($"{baseAPIUrl}/{name}/{packageCode}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServicePackage>(apiResponse)!;
        }

        public async Task<ServicePackage> EditAnExistServicePackage(ServicePackage servicePackage)
        {
            var json = JsonConvert.SerializeObject(servicePackage);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var apiRequest = await _httpClient.PutAsync($"{baseAPIUrl}/{name}", stringContent);
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServicePackage>(apiResponse)!;
        }

        public async Task<List<ServicePackage>> GetServicePackageList(int limit)
        {
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}?limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ServicePackage>>(apiResponse)!;
        }

        public async Task<ServicePackage> GetTheServicePackageByPackageCode(string packageCode)
        {
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{packageCode}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServicePackage>(apiResponse)!;
        }

        public async Task<List<ServicePackage>> GetTheServicePackagesBySearchString(string str, int limit)
        {
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/filter?str={str}&limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ServicePackage>>(apiResponse)!;
        }
    }
}
