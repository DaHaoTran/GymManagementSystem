using Client_FAU.Business.Interfaces;
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
        private IHttpContextAccessor? HttpContextAccessor { get; set; }
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        [Inject]
        private ISweetAlertService? Swal { get; set; }
        [SupplyParameterFromForm]
        private Salary? Model { get; set; } = new();

        private List<Salary>? salaries;
        private string sessionName = "salaries";
        private string message = string.Empty;
        private bool isLoading = false;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;

            await LoadSalaryList();
            await LoadServicePackageList();

            isLoading = false;
        }

        private async Task LoadSalaryList()
        {
            var data = HttpContextAccessor!.HttpContext!.Session.GetString(sessionName);
            if(data!= null)
            {
                salaries = JsonConvert.DeserializeObject<List<Salary>>(data);
                return;
            }
            var getSalaries = await SalaryBsn!.GetSalaryList(12);
            salaries = getSalaries;
            HttpContextAccessor!.HttpContext!.Session.SetString(sessionName, JsonConvert.SerializeObject(getSalaries));
        }

        private void ClearForm() => Model = new();

        private void SetSalaryProperties()
        {
            Model!.SalaryCode = "SA";
            Model.PricesApply = double.Parse(Model.GetPricesApply);
            Model.UpdateDate = DateTime.UtcNow;
        }

        private async Task AddSalaryDataBase()
        {
            isLoading = true;
            try
            {
                SetSalaryProperties();
                var result = await SalaryBsn!.AddANewSalary(Model!);

                if (result != null)
                {
                    message = "Add new salary successfully";
                    UpdateSalariesData(result);
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
            isLoading = false;
            await JSRuntime!.InvokeVoidAsync("UndisplaySalarySample");
        }

        private async Task EditSalaryDataBase(Salary salary)
        {
            isLoading = true;
            if(salary.GetPricesApply == salary.PricesApply.ToString())
            {
                isLoading = false;
                return;
            }

            try
            {
                salary.PricesApply = double.Parse(salary.GetPricesApply);
                var result = await SalaryBsn!.EditAnExistSalary(salary);

                if(result != null)
                {
                    message = $"Edit {salary.SalaryType} Successfully";
                    UpdateSalariesData(salary);
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
            isLoading = false;
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
                isLoading = true;
                try
                {
                    var result = await SalaryBsn!.DeleteAnExistSalary(salaryCode);
                    if (result != null)
                    {
                        message = $"Delete {result.SalaryType} Successfully";
                        RemoveSalariesFromData(result);
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
                isLoading = false;
            }
        }

        private void UpdateSalariesData(Salary salary)
        {
            if(salary == null) { return; }
            var getSalary = salaries.Where(x => x.SalaryCode == salary.SalaryCode).FirstOrDefault();
            if(getSalary == default || getSalary == null)
            {
                salaries.Insert(0, salary);
            }
            else
            {
                int index = salaries.IndexOf(getSalary);
                salaries[index] = salary;
            }

            HttpContextAccessor!.HttpContext!.Session.SetString(sessionName, JsonConvert.SerializeObject(salaries));
        }

        private void RemoveSalariesFromData(Salary salary)
        {
            if (salary == null) { return; }
            var getSalary = salaries!.Where(x => x.SalaryCode == salary.SalaryCode).FirstOrDefault();
            if (getSalary == null || getSalary == default) { return; }
            salaries!.Remove(getSalary);
            HttpContextAccessor!.HttpContext!.Session.SetString(sessionName, JsonConvert.SerializeObject(salaries));
        }

        private void ShowInvalidMessage()
        {
            message = "The inputs are invalid, check again !";
        }
    }
}
