﻿using Client_FAU.Business.Interfaces;
using Client_FAU.Extensions;
using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SweetAlert2;
using System.Threading.Tasks;

namespace Client_FAU.Components.Pages
{
    public partial class LoginPage
    {
        [Inject]
        private Account_Int? AccountBsn { get; set; }
        [Inject]
        private Token_Int? TokenBsn { get; set; }
        [Inject]
        private ISweetAlertService? Swal { get; set; }
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        [Inject]
        private NavigationManager? NavigationManager { get; set; }
        [SupplyParameterFromForm]
        private Login Model { get; set; } = new();

        private async Task SetSessionByName(string name, string value)
        {
            if (string.IsNullOrEmpty(name)) { return; }
            if (string.IsNullOrEmpty(value)) { return; }
            await JSRuntime!.InvokeVoidAsync("localStorage.setItem", name, value);
        }

        private void ClearForm() => Model = new();

        private void SetCheckState()
        {
            Model.Password = !string.IsNullOrEmpty(Model.Password) ? PasswordManipulates.EncryptPassword(Model.Password) : Model.Password;
        }

        private async Task CheckLoginDatabase()
        {
            Load.IsLoggingIn = true;
            try
            {
                SetCheckState();
                var result = await AccountBsn!.ValidateAccount(Model);
                if(result != null)
                {
                    if(result.AccountCode.Substring(0, 2).Contains("AD", StringComparison.OrdinalIgnoreCase))
                    {
                        //Set generate state
                        result.GetRoleName = "string";
                        result.GetSalaryType = "string";
                        
                        Validation.AccountCode = result.AccountCode;
                        Validation.FullName = result.FullName;

                        var token = await GenerateToken(result);
                        if(token == null) { return; }
                        Validation.JwtToken = token;

                        Validation.IsLoggedIn = true;
                    } else
                    {
                        await ShowMessage("This account does not allow to access");
                    } 
                } else
                {
                    await ShowMessage("Wrong code or password");
                }
                Load.IsLoggingIn = false;
                NavigationManager!.NavigateTo("/", forceLoad: true);
                StateHasChanged();
            } catch { }
        }

        private async Task ShowMessage(string message)
        {
            SweetAlertResult swalResult = await Swal!.FireAsync(new SweetAlertOptions
            {
                Title = "Validation",
                Text = message.Trim(),
                Icon = SweetAlertIcon.Info,
                ShowConfirmButton = true,
            });
        }

        private async Task<string> GenerateToken(Account account)
        {
            return await TokenBsn!.GenerateJwtToken(account);
        }

        private async Task<string> SolveToken(string token)
        {
            return await TokenBsn!.SolveToken(token);
        }
    }
}
