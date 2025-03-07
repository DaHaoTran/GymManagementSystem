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
        private IJSRuntime? JSRuntime { get; set; }
        [SupplyParameterFromForm]
        private Account? Model { get; set; } = new();

        private string str = string.Empty;

        private void ClearForm() => Model = new();

        private void SetModelState(ModalState.State state)
        {
            ModalState.current_state = state;
        }

        private void SetEditState(Account account)
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

            var getRole = Lists.roles!.Where(x => x.RoleName == Model.GetRoleName).FirstOrDefault();
            if(getRole == null || getRole == default) 
            {
                Notification.message = "Role is missing !";
                return; 
            }
            Model.RoleId = getRole.OrderNumber;

            var getSalary = Lists.salaries!.Where(x => x.SalaryType == Model.GetSalaryType).FirstOrDefault();
            if (getSalary == null || getSalary == default)
            {
                Notification.message = "Salary type is missing !";
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

            var getRole = Lists.roles!.Where(x => x.RoleName == Model.GetRoleName).FirstOrDefault();
            if (getRole == null || getRole == default)
            {
                Notification.message = "Role is missing !";
                return;
            }
            Model.RoleId = getRole.OrderNumber;

            var getSalary = Lists.salaries!.Where(x => x.SalaryType == Model.GetSalaryType).FirstOrDefault();
            if (getSalary == null || getSalary == default)
            {
                Notification.message = "Salary type is missing !";
                return;
            }
            Model.SalaryCode = getSalary.SalaryCode;
        }

        private void SetPasswordModelWithChangedValue()
        {
            if (Lists.accounts == null) { return; }

            var getAccount = Lists.accounts.Where(x => x.AccountCode == Model!.AccountCode).FirstOrDefault();
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
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateAccountsData(result);
                }
                else
                {
                    await JSRuntime!.InvokeVoidAsync("PlayErrorAudio");
                }
            }
            catch (Exception ex)
            {
                Notification.message = ex.Message;
            }
            ClearForm();
            Thread.Sleep(500);
            Load.IsLoading  = false;

            Thread.Sleep(100);
            await JSRuntime!.InvokeVoidAsync("Reload");
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
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateAccountsData(result);
                }
                else
                {
                    await JSRuntime!.InvokeVoidAsync("PlayErrorAudio");
                }
            }
            catch (Exception ex)
            {
                Notification.message = ex.Message;
            }
            ClearForm();
            Thread.Sleep(500);
            Load.IsLoading  = false;
            await JSRuntime!.InvokeVoidAsync("CloseEditModal");

            Thread.Sleep(100);
            await JSRuntime!.InvokeVoidAsync("Reload");
        }

        private async Task EditAccountDataBase()
        {
            Load.IsLoading  = true;
            try
            {
                SetAccountProperties2();
                var result = await AccountBsn!.EditAnExistAccount(Model!);
                if (result != null)
                {
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateAccountsData(result);
                }
                else
                {
                    await JSRuntime!.InvokeVoidAsync("PlayErrorAudio");
                }
            }
            catch (Exception ex)
            {
                Notification.message = ex.Message;
            }
            ClearForm();
            Thread.Sleep(500);
            Load.IsLoading  = false;
            await JSRuntime!.InvokeVoidAsync("CloseEditModal");

            Thread.Sleep(100);
            await JSRuntime!.InvokeVoidAsync("Reload");
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

        private async Task FindAccountsInDatabase()
        {
            if(string.IsNullOrEmpty(str)) { return; }
            Load.IsLoading = true;
            try
            {
                var result = await AccountBsn!.GetTheAccountsBySearchString(str.Trim(), 9);
                if (result != null)
                {
                    Lists.accounts = result;
                }
                else
                {
                    Lists.accounts = new List<Account>();
                }
            }
            catch (Exception ex)
            {
                Notification.message = ex.Message;
            }
            Load.IsLoading = false;

            Thread.Sleep(100);
            await JSRuntime!.InvokeVoidAsync("Reload");
        }

        private void UpdateAccountsData(Account account)
        {
            if(account == null) { return; }
            var getAccount = Lists.accounts!.Where(x => x.AccountCode == account.AccountCode).FirstOrDefault();
            if(getAccount  == default || getAccount == null)
            {
                Lists.accounts!.Insert(0, account);
                if (Lists.accounts.Count() >= 9) { Lists.accounts.RemoveAt(Lists.accounts.Count() - 1); }
            }
            else
            {
                var index = Lists.accounts!.IndexOf(getAccount);
                Lists.accounts[index] = account;
            }
        }
    }
}
