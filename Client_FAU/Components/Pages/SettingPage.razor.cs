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

        private List<Salary>? salaries;
        private string sessionName = "salaries";

        protected override async Task OnInitializedAsync()
        {
            await LoadSalaryList();
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
    }
}
