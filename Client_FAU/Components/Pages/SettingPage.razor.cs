using Blazored.LocalStorage;
using Client_FAU.Business.Interfaces;
using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;
using Radzen;
using SweetAlert2;
using System.Threading.Tasks;

namespace Client_FAU.Components.Pages
{
    public partial class SettingPage
    {
        [Inject]
        private Salary_Int? SalaryBsn { get; set; }
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        [Inject]
        private ISweetAlertService? Swal { get; set; }
        [SupplyParameterFromForm]
        private Salary? Model { get; set; } = new();

        private void ClearForm() => Model = new();

        private void SetSalaryProperties()
        {
            Model!.SalaryType = Model.SalaryType!.Trim();
            Model!.SalaryCode = "SA";
            Model.PricesApply = double.Parse(Model.GetPricesApply.Trim());
            Model.UpdateDate = DateTime.UtcNow;
        }

        private async Task AddSalaryDataBase()
        {
            Load.IsLoading  = true;
            try
            {
                SetSalaryProperties();
                var result = await SalaryBsn!.AddANewSalary(Model!);

                if (result != null)
                {
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateSalariesData(result);
                }
                else
                {
                    await JSRuntime!.InvokeVoidAsync("PlayErrorAudio");
                }
            } catch (Exception ex)
            {
                Notification.message = ex.Message;
            }
            ClearForm();
            Thread.Sleep(500);
            Load.IsLoading  = false;
            await JSRuntime!.InvokeVoidAsync("UndisplaySalarySample");

            Thread.Sleep(100);
            await JSRuntime!.InvokeVoidAsync("Reload");
        }

        private async Task EditSalaryDataBase(Salary salary)
        {
            Load.IsLoading  = true;

            if(Lists.salaries != null)
            {
                var getSalary = Lists.salaries.Where(x => x.SalaryCode == salary.SalaryCode).FirstOrDefault();
                if (getSalary != null)
                {
                    if(getSalary.PricesApply == salary.PricesApply)
                    {
                        Load.IsLoading  = false;
                        return;
                    }
                }
            }

            //Else update the salary
            try
            {
                var result = await SalaryBsn!.EditAnExistSalary(salary);

                if(result != null)
                {
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateSalariesData(salary);
                }
                else
                {
                    await JSRuntime!.InvokeVoidAsync("PlayErrorAudio");
                }
            } catch (Exception ex)
            {
                Notification.message = ex.Message;
            }
            ClearForm();
            Thread.Sleep(500);
            Load.IsLoading  = false;

            Thread.Sleep(100);
            await JSRuntime!.InvokeVoidAsync("Reload");
        }
        
        private async Task DeleteSalaryDataBase(string salaryCode)
        {
            SweetAlertResult swalResult = await Swal!.FireAsync(new SweetAlertOptions
            {
                Title = "Are you sure?",
                Text = "You won't be able to revert this!",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "Yes, delete it!",
                CancelButtonText = "No, cancel!"
            });

            if(swalResult.IsConfirmed)
            {
                Load.IsLoading  = true;
                try
                {
                    var result = await SalaryBsn!.DeleteAnExistSalary(salaryCode);
                    if (result != null)
                    {
                        await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                        RemoveSalariesFromData(result);
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
                Thread.Sleep(500);
                Load.IsLoading  = false;

                Thread.Sleep(100);
                await JSRuntime!.InvokeVoidAsync("Reload");
            }
        }

        private void UpdateSalariesData(Salary salary)
        {
            if(salary == null) { return; }
            var getSalary = Lists.salaries.Where(x => x.SalaryCode == salary.SalaryCode).FirstOrDefault();
            if(getSalary == default || getSalary == null)
            {
                Lists.salaries.Insert(0, salary);
            }
            else
            {
                int index = Lists.salaries.IndexOf(getSalary);
                Lists.salaries[index] = salary;
            }
        }

        private void RemoveSalariesFromData(Salary salary)
        {
            if (salary == null) { return; }
            var getSalary = Lists.salaries.Where(x => x.SalaryCode == salary.SalaryCode).FirstOrDefault();
            if (getSalary == null || getSalary == default) { return; }
            Lists.salaries.Remove(getSalary);
        }

        private void ShowInvalidMessage()
        {
            Notification.message = "The inputs are invalid, check again !";
        }
    }
}
