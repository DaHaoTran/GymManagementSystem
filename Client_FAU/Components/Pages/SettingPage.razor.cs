using Client_FAU.Business.Interfaces;
using Microsoft.AspNetCore.Components;
using Models;
using Newtonsoft.Json;

namespace Client_FAU.Components.Pages
{
    public partial class SettingPage
    {
        [Inject]
        private Salary_Int? SalaryBsn { get; set; }
        [Inject]
        private IHttpContextAccessor? HttpContextAccessor { get; set; }
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

        private async Task AddSalariesDataBase()
        {
            try
            {
                SetSalaryProperties();
                var result = await SalaryBsn!.AddANewSalary(Model!);

                if (result != null)
                {
                    message = "Add new Salary Successfully";
                    UpdateSalariesData(result);
                }
                else
                {
                    message = "Add new Salary failed. There may be problem !";
                }
            } catch (Exception ex)
            {
                message = ex.Message;
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

        private void ShowInvalidMessage()
        {
            message = "The inputs are invalid, Check again !";
        }
    }
}
