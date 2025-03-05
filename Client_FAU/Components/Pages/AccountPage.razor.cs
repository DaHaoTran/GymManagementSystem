using Blazored.LocalStorage;
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
        private ILocalStorageService? LocalStorage { get; set; }
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        [SupplyParameterFromForm]
        private Account? Model { get; set; } = new();

        private IEnumerable<Role>? roles = new List<Role>();
        private IEnumerable<Salary>? salaries = new List<Salary>();
        private List<Account>? accounts;
        private string message = string.Empty;

        protected override void OnInitialized()
        {
            Load.IsLoading = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                await LoadAccountList();
                await LoadRoleList();
                await LoadSalaryList();

                Thread.Sleep(1000);
                Load.IsLoading = false;
                StateHasChanged();
            }
        }

        private void ClearForm() => Model = new();

        private async Task LoadRoleList()
        {
            var data = await LocalStorage!.GetItemAsync<IEnumerable<Role>>(SessionNames.Roles);
            if(data == null) { return; }
            roles = data;
        }

        private async Task LoadSalaryList()
        {
            var data = await LocalStorage!.GetItemAsync<IEnumerable<Salary>>(SessionNames.Salaries);
            if (data == null) { return; }
            salaries = data;
        }

        private async Task LoadAccountList()
        {
            var data = await LocalStorage!.GetItemAsync<List<Account>>(SessionNames.Accounts);
            if(data != null)
            {
                accounts = data;
                return;
            }
            var getAccounts = await AccountBsn!.GetAccountList(9);
            accounts = getAccounts;
            await LocalStorage.SetItemAsync(SessionNames.Accounts, getAccounts);
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

        private async Task SetAccountProperties2()
        {
            Model!.FullName = Model.FullName.Trim();
            Model.PhoneNumber = Model.PhoneNumber.Trim();
            Model.IdNumber = Model.IdNumber.Trim();
            Model.LivingAt = Model.LivingAt.Trim();
            await SetPasswordModelWithChangedValue();
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

        private async Task SetPasswordModelWithChangedValue()
        {
            var data = await LocalStorage!.GetItemAsync<List<Account>>(SessionNames.Accounts);
            if (data == null) { return; }

            var getAccounts = data;
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
            Load.IsLoading  = true;
            try
            {
                var result = await AccountBsn!.EditAnExistAccount(account);
                if (result != null)
                {
                    message = $"Edit {account.AccountCode} successfully !";
                    await UpdateAccountsData(result);
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
            Thread.Sleep(500);
            Load.IsLoading  = false;
            StateHasChanged();
        }

        private async Task AddAccountDataBase()
        {
            Load.IsLoading  = true;
            try
            {
                SetAccountProperties();
                var result = await AccountBsn!.AddANewAccount(Model!);
                if (result != null)
                {
                    message = "Add new account successfully !";
                    await UpdateAccountsData(result);
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
            Thread.Sleep(500);
            Load.IsLoading  = false;
            await JSRuntime!.InvokeVoidAsync("CloseEditModal");
            StateHasChanged();
        }

        private async Task EditAccountDataBase()
        {
            Load.IsLoading  = true;
            try
            {
                await SetAccountProperties2();
                var result = await AccountBsn!.EditAnExistAccount(Model!);
                if (result != null)
                {
                    message = $"Edit {Model!.AccountCode} successfully !";
                    await UpdateAccountsData(result);
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
            Thread.Sleep(500);
            Load.IsLoading  = false;
            await JSRuntime!.InvokeVoidAsync("CloseEditModal");
            StateHasChanged();
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

        private async Task UpdateAccountsData(Account account)
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

            await LocalStorage!.SetItemAsync(SessionNames.Accounts, accounts);
        }
    }
}
