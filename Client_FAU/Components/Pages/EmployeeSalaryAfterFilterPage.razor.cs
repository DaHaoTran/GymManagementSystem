using Client_FAU.Business.Interfaces;
using Client_FAU.Components.Ingredients;
using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using Models;
using System.Threading.Tasks;

namespace Client_FAU.Components.Pages
{
    public partial class EmployeeSalaryAfterFilterPage
    {
        [Inject]
        private EmployeeSalary_Int? ESBsn {  get; set; }
        [Inject]
        private NavigationManager? Navigation { get; set; }
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        [SupplyParameterFromForm]
        private EmployeeSalary? Model { get; set; } = new();


        private IEnumerable<EmployeeSalary>? data;
        private bool isLoading = false;

        async Task ShowLoading()
        {
            isLoading = true;

            await Task.Yield();

            isLoading = false;
        }

        protected override async Task OnInitializedAsync()
        {
            int month = 0, year = 0;
            var uri = new Uri(Navigation!.Uri);
            var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
            if (query.TryGetValue("month", out var value))
            {
                month = !string.IsNullOrEmpty(value) ? int.Parse(value!) : 0;
            }
            if (query.TryGetValue("year", out var value2))
            {
                year = !string.IsNullOrEmpty(value2) ? int.Parse(value2!) : 0;
            }

            if (month == 0 || year == 0) { return; }
            await FindEmployeeSalariesDatabase(month, year);
        }

        private async Task FindEmployeeSalariesDatabase(int month, int year)
        {
            try
            {
                var result = await ESBsn!.GetTheEmployeeSalariesByMonth(month, year);

                if(result != null)
                {
                    Lists.employeeSalaries = result;
                } else
                {
                    Lists.employeeSalaries = new List<EmployeeSalary>();
                }

                data = Lists.employeeSalaries;
            } catch (Exception ex)
            {
                Notification.message = ex.Message;
            }

            //Thread.Sleep(100);
            //await JSRuntime!.InvokeVoidAsync("Reload");
        }
    }
}
