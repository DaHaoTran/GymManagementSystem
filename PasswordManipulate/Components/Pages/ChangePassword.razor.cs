using PasswordManipulate.Business.Interfaces;
using Microsoft.AspNetCore.Components;
using Models;
using PasswordManipulate.Data;
using PasswordManipulate.Extensions;
using PasswordManipulate.Variables;

namespace PasswordManipulate.Components.Pages
{
    public partial class ChangePassword
    {
        [Inject]
        private Account_Int? AccountSvc { get; set; }
        [Inject]
        private Token_Int? TokenSvc { get; set; }
        [SupplyParameterFromForm]
        private LoginCP Model { get; set; } = new();
        private static string message = string.Empty;

        private Login SetNewLogin()
        {
            Login login = new();
            login.AccountCode = Model.AccountCode;
            login.Password = PasswordManipulates.EncryptPassword(Model.Password);
            return login;
        }

        private Account SetUpdateAccount(Account account)
        {
            account.Password = PasswordManipulates.EncryptPassword(Model.NewPassword);
            account.GetRoleName = "string";
            account.GetSalaryType = "string";
            return account;
        }

        private async Task UpdateAccountForDatabase()
        {
            try
            {
                var getAccount = await AccountSvc!.ValidateAccount(SetNewLogin());
                if (getAccount == null)
                {
                    message = "Invalid code or password";
                    return;
                }

                Validation.Token = await TokenSvc!.GenerateJwtToken(getAccount);
                var updateAccount = await AccountSvc!.EditAnExistAccount(SetUpdateAccount(getAccount));
                if (updateAccount == null)
                {
                    message = "Error when updating account. Please try again !";
                    return;
                }

                State.isConfirm = true;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                message = ex.Message;
                StateHasChanged();
            }
        }
    }
}
