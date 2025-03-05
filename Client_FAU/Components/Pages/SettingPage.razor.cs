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
        private ILocalStorageService? LocalStorage { get; set; }
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        [Inject]
        private ISweetAlertService? Swal { get; set; }
        [SupplyParameterFromForm]
        private Salary? Model { get; set; } = new();

        private List<Salary>? salaries;
        private string message = string.Empty;

        protected override void OnInitialized()
        {
            Load.IsLoading = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                await LoadSalaryList();
                await LoadServicePackageList();

                Thread.Sleep(1000);
                Load.IsLoading = false;
                StateHasChanged();
            }
        }

        private async Task LoadSalaryList()
        {
            var data = await LocalStorage!.GetItemAsync<List<Salary>>(SessionNames.Salaries);
            if(data!= null)
            {
                salaries = data;
                return;
            }
            var getSalaries = await SalaryBsn!.GetSalaryList(0);
            salaries = getSalaries;
            await LocalStorage.SetItemAsync(SessionNames.Salaries, getSalaries);
        }

        private void ClearForm() => Model = new();

        private void SetSalaryProperties()
        {
            Model!.SalaryType = Model.SalaryType!.Trim();
            Model!.SalaryCode = "SA";
            Model.PricesApply = double.Parse(Model.GetPricesApply);
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
                    message = "Add new salary successfully";
                    await UpdateSalariesData(result);
                }
                else
                {
                    message = "Add new salary failed. Problems arise !";
                }
            } catch (Exception ex)
            {
                message = ex.Message;
            }
            ClearForm();
            Thread.Sleep(500);
            Load.IsLoading  = false;
            await JSRuntime!.InvokeVoidAsync("UndisplaySalarySample");
            StateHasChanged();
        }

        private async Task EditSalaryDataBase(Salary salary)
        {
            Load.IsLoading  = true;

            //Get the salaries from session and check if the values salary
            var data = await LocalStorage!.GetItemAsync<List<Salary>>(SessionNames.Salaries);
            if(data != null)
            {
                var temSalaries = data;
                var getSalary = temSalaries!.Where(x => x.SalaryCode == salary.SalaryCode).FirstOrDefault();
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
                    message = $"Edit {salary.SalaryType} Successfully";
                    await UpdateSalariesData(salary);
                }
                else
                {
                    message = "Edit salary failed. Problems arise !";
                }
            } catch (Exception ex)
            {
                message = ex.Message;
            }
            ClearForm();
            Thread.Sleep(500);
            Load.IsLoading  = false;
            StateHasChanged();
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
                        message = $"Delete {result.SalaryType} Successfully";
                        await RemoveSalariesFromData(result);
                    }
                    else
                    {
                        message = "Delete salary failed. Problems arise !";
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
                Thread.Sleep(500);
                Load.IsLoading  = false;
                StateHasChanged();
            }
        }

        private async Task UpdateSalariesData(Salary salary)
        {
            if(salary == null) { return; }
            var getSalary = salaries!.Where(x => x.SalaryCode == salary.SalaryCode).FirstOrDefault();
            if(getSalary == default || getSalary == null)
            {
                salaries!.Insert(0, salary);
            }
            else
            {
                int index = salaries!.IndexOf(getSalary);
                salaries[index] = salary;
            }

            await LocalStorage!.SetItemAsync(SessionNames.Salaries, salaries);
        }

        private async Task RemoveSalariesFromData(Salary salary)
        {
            if (salary == null) { return; }
            var getSalary = salaries!.Where(x => x.SalaryCode == salary.SalaryCode).FirstOrDefault();
            if (getSalary == null || getSalary == default) { return; }
            salaries!.Remove(getSalary);
            await LocalStorage!.SetItemAsync(SessionNames.Salaries, salaries);
        }

        private void ShowInvalidMessage()
        {
            message = "The inputs are invalid, check again !";
        }
    }
}
