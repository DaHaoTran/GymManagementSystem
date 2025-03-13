using Client_FAU.Business.Interfaces;
using Client_FAU.Extensions;
using Models;
using Newtonsoft.Json;
using System.Text;

namespace Client_FAU.Business.Implements
{
    public class Account_Imp : Account_Int
    {
        private static string? baseAPIUrl;
        private readonly HttpClient _httpClient;
        private readonly string name = "accounts";
        private readonly Jwt_Int _jwt;
        public Account_Imp(IConfiguration configuration, HttpClient httpClient, Jwt_Int jwt)
        {
            var apiUrl = configuration["BaseAPIUrl"];
            baseAPIUrl = apiUrl != null ? apiUrl : string.Empty;
            _httpClient = httpClient;
            _jwt = jwt;
        }

        public async Task<Account> AddANewAccount(Account account)
        {
            var json = JsonConvert.SerializeObject(account);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.PostAsync($"{baseAPIUrl}/{name}", stringContent);
            if(!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Account>(apiResponse)!;
        }

        public async Task<Account> DeleteAnExistAccount(string accountCode)
        {
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.DeleteAsync($"{baseAPIUrl}/{name}/{accountCode}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Account>(apiResponse)!;
        }

        public async Task<Account> EditAnExistAccount(Account account)
        {
            var json = JsonConvert.SerializeObject(account);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.PutAsync($"{baseAPIUrl}/{name}", stringContent);
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Account>(apiResponse)!;
        }

        public async Task<List<Account>> GetAccountList(int limit)
        {
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}?limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Account>>(apiResponse)!;
        }

        public async Task<Account> GetTheAccountByAccountCode(string accountCode)
        {
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/{accountCode}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Account>(apiResponse)!;
        }

        public async Task<List<Account>> GetTheAccountsBySearchString(string str, int limit)
        {
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.GetAsync($"{baseAPIUrl}/{name}/filter?str={str}&limit={limit}");
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Account>>(apiResponse)!;
        }

        public async Task<Account> ValidateAccount(Login login)
        {
            var json = JsonConvert.SerializeObject(login);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _jwt.SetAuthorizationHeaderAsync("jwtToken");
            var apiRequest = await _httpClient.PostAsync($"{baseAPIUrl}/{name}/validate", stringContent);
            if (!apiRequest.IsSuccessStatusCode) { return null!; }
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Account>(apiResponse)!;
        }
    }
}
