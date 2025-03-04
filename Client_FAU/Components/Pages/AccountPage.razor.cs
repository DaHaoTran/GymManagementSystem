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

        private void SetModelState(ModalState.State state)
        {
            ModalState.current_state = state;
        }

        private void SetModelEdit(Account account)
        {
            Model!.AccountCode = account.AccountCode;
            Model.FullName = account.FullName;
            Model.IdNumber = account.IdNumber;
            Model.RoleId = account.RoleId;
            Model.Age = account.Age;
            Model.PhoneNumber = account.PhoneNumber;
            Model.LivingAt = account.LivingAt;
            Model.Password = account.Password;
            Model.UpdateBy = account.UpdateBy;
            Model.SalaryCode = account.SalaryCode;
            Model.IsDeleted = account.IsDeleted;
            Model.GetRoleName = account.GetRoleName;
            Model.GetSalaryType = account.GetSalaryType;
            SetModelState(ModalState.State.Edit);
        }

        private void SetAddState()
        {
            Model = new();
            SetModelState(ModalState.State.Add);
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

        private void SetAccountProperties2()
        {
            Model!.FullName = Model.FullName.Trim();
            Model.PhoneNumber = Model.PhoneNumber.Trim();
            Model.IdNumber = Model.IdNumber.Trim();
            Model.LivingAt = Model.LivingAt.Trim();
            SetPasswordModelWithChangedValue();
            Model.UpdateBy = "AD00000001";

            var getRole = roles!.Where(x => x.RoleName == Model.GetRoleName).FirstOrDefault();
            if (getRole == null || getRole == default)
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

        private void SetPasswordModelWithChangedValue()
        {
            var data = HttpContextAccessor!.HttpContext!.Session.GetString(SessionNames.Accounts);
            if (data == null) { return; }

            var getAccounts = JsonConvert.DeserializeObject<List<Account>>(data);
            var getAccount = getAccounts!.Where(x => x.AccountCode == Model!.AccountCode).FirstOrDefault();
            if (getAccount == null || getAccount == default) { return; }

            if(Model!.Password == getAccount.Password) { return; }

            if (getAccount.Password == PasswordManipulates.EncryptPassword(Model!.Password.Trim())) 
            {
                Model.Password = getAccount.Password;
                return; 
            }

            Model.Password = PasswordManipulates.EncryptPassword(Model!.Password.Trim());

        }

        private async Task EditAccountStateDataBase(Account account)
        {
            account.IsDeleted = !account.IsDeleted;
            isLoading = true;
            try
            {
                var result = await AccountBsn!.EditAnExistAccount(account);
                if (result != null)
                {
                    message = $"Edit {account.AccountCode} successfully !";
                    UpdateAccountsData(result);
                }
                else
                {
                    message = $"Edit {account.AccountCode} failed. Problems arise !";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            ClearForm();
            isLoading = false;
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

        private async Task EditAccountDataBase()
        {
            isLoading = true;
            try
            {
                SetAccountProperties2();
                var result = await AccountBsn!.EditAnExistAccount(Model!);
                if (result != null)
                {
                    message = $"Edit {Model!.AccountCode} successfully !";
                    UpdateAccountsData(result);
                }
                else
                {
                    message = $"Edit {Model!.AccountCode} failed. Problems arise !";
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

        private async Task UpdateAccountDataBase()
        {
            switch(ModalState.current_state)
            {
                case ModalState.State.Add:
                    await AddAccountDataBase();
                    break;
                case ModalState.State.Edit:
                    await EditAccountDataBase();
                    break;
                default:
                    break;
            }
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
