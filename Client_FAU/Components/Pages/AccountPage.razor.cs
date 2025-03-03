using Client_FAU.Business.Interfaces;
using Client_FAU.Extensions;
using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Client_FAU.Components.Pages
{
    public partial class AccountPage
    {
        [Inject]
        private Account_Int? AccountBsn { get; set; }
        [Inject]
        private IHttpContextAccessor? HttpContextAccessor { get; set; }
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        [SupplyParameterFromForm]
        private Account? Model { get; set; } = new();

        private IEnumerable<Role>? roles = new List<Role>();
        private IEnumerable<Salary>? salaries = new List<Salary>();
        private List<Account>? accounts;
        private bool isLoading = false;
        private string message = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;

            await LoadAccountList();
            LoadRoleList();
            LoadSalaryList();

            isLoading = false;
        }

        private void ClearForm() => Model = new();

        private void LoadRoleList()
        {
            var data = HttpContextAccessor!.HttpContext!.Session.GetString(SessionNames.Roles);
            if(data == null) { return; }
            roles = JsonConvert.DeserializeObject<IEnumerable<Role>>(data);
        }

        private void LoadSalaryList()
        {
            var data = HttpContextAccessor!.HttpContext!.Session.GetString(SessionNames.Salaries);
            if (data == null) { return; }
            salaries = JsonConvert.DeserializeObject<IEnumerable<Salary>>(data);
        }

        private async Task LoadAccountList()
        {
            var data = HttpContextAccessor!.HttpContext!.Session.GetString(SessionNames.Accounts);
            if(data != null)
            {
                accounts = JsonConvert.DeserializeObject<List<Account>>(data);
                return;
            }
            var getAccounts = await AccountBsn!.GetAccountList(9);
            accounts = getAccounts;
            HttpContextAccessor!.HttpContext!.Session.SetString(SessionNames.Accounts, JsonConvert.SerializeObject(getAccounts));
        }

        private void SetAccountProperties()
        {
            Model!.AccountCode = Model.GetRoleName.Trim().Substring(0, 2).ToUpper();
            Model.FullName = Model.FullName.Trim();
            Model.PhoneNumber = Model.PhoneNumber.Trim();
            Model.IdNumber = Model.IdNumber.Trim();
            Model.LivingAt = Model.LivingAt.Trim();
            Model.Password = PasswordManipulates.EncryptPassword(Model.Password.Trim());
            Model.UpdateBy = "AD00000001";

            var getRole = roles!.Where(x => x.RoleName == Model.GetRoleName).FirstOrDefault();
            if(getRole == null || getRole == default) 
            {
                message = "Role is missing !";
                return; 
            }
            Model.RoleId = getRole.OrderNumber;

            var getSalary = salaries!.Where(x => x.SalaryType == Model.GetSalaryType).FirstOrDefault();
            if (getSalary == null || getSalary == default)
            {
                message = "Salary type is missing !";
                return;
            }
            Model.SalaryCode = getSalary.SalaryCode;
        }

        private async Task AddAccountDataBase()
        {
            isLoading = true;
            try
            {
                SetAccountProperties();
                var result = await AccountBsn!.AddANewAccount(Model!);
                if (result != null)
                {
                    message = "Add new account successfully !";
                    UpdateAccountsData(result);
                }
                else
                {
                    message = "Add new account failed. Problems arise !";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            ClearForm();
            isLoading = false;
            await JSRuntime!.InvokeVoidAsync("CloseEditModal");
        }

        private void UpdateAccountsData(Account account)
        {
            if(account == null) { return; }
            var getAccount = accounts!.Where(x => x.AccountCode == account.AccountCode).FirstOrDefault();
            if(getAccount  == default || getAccount == null)
            {
                accounts!.Insert(0, account);
                if (accounts.Count() >= 9) { accounts.RemoveAt(accounts.Count() - 1); }
            }
            else
            {
                var index = accounts!.IndexOf(getAccount);
                accounts[index] = account;
            }

            HttpContextAccessor!.HttpContext!.Session.SetString(SessionNames.Accounts, JsonConvert.SerializeObject(accounts));
        }
    }
}
