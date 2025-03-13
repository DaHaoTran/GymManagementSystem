using Client_FAU.Components.Pages;
using Client_FAU.Business.Interfaces;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using Models;

namespace Client_FAU.Business.Implements
{
    public class Branch_Imp : Branch_Int
    {
        private static string? baseAPIUrl;
        private readonly HttpClient _httpClient;
        private readonly string name = "branches";
        private readonly Jwt_Int _jwt;
        public Branch_Imp(IConfiguration configuration, HttpClient httpClient, Jwt_Int jwt)
        {
            var apiUrl = configuration["BaseAPIUrl"];
            baseAPIUrl = apiUrl != null ? apiUrl : string.Empty;
            _httpClient = httpClient;
            _jwt = jwt;
        }

        public async Task<Branch> AddNewBranch(Branch branch)
        {
            var json = JsonConvert.SerializeObject(branch);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.PostAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Branch>(apiResponse)!;
        }

        public async Task<Branch> DeleteAnExistBranch(string branchCode)
        {
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.DeleteAsync($"{baseAPIUrl}/{name}/{branchCode}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Branch>(apiResponse)!;
        }

        public async Task<Branch> EditAnExistBranch(Branch branch)
        {
            var json = JsonConvert.SerializeObject(branch);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.PutAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Branch>(apiResponse)!;
        }

        public async Task<List<Branch>> GetBranchList(int limit)
        {
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}?limit={limit}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Branch>>(apiResponse)!;
        }

        public async Task<Branch> GetTheBranchByBranchCode(string branchCode)
        {
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{branchCode}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Branch>(apiResponse)!;
        }

        public async Task<List<Branch>> GetTheBranchesBySearchString(string str, int limit)
        {
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/filter?str={str}&limit={limit}");
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Branch>>(apiResponse)!;
        }
    }
}
