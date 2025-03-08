using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;

namespace Client_FAU.Components.Pages
{
    public partial class EmployeeSalaryPage
    {
        [Inject]
        private NavigationManager? Navigation { get; set; }

        private int currentYear = DateTime.UtcNow.Year;
        private int thisYear = DateTime.UtcNow.Year;

        protected override void OnInitialized()
        {
            Lists.employeeSalaries = new();
        }

        private void NavigateToList(int month)
        {
            Navigation!.NavigateTo($"/employee-salaries-filter?month={month}&year={currentYear}");
        }
    }
}
