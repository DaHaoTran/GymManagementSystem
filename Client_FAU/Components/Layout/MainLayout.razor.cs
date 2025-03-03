using Client_FAU.Business.Interfaces;
using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Client_FAU.Components.Layout
{
    public partial class MainLayout
    {
        [Inject]
        private Role_Int? RoleBsn { get; set; }
        [Inject]
        private Salary_Int? SalaryBsn { get; set; }
        [Inject]
        private ServicePackage_Int? SPBsn { get; set; }
        [Inject]
        private IHttpContextAccessor? HttpContextAccessor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetRoleList();
            await GetSalaryList();
            await GetServicePackageList();
        }

        private async Task GetRoleList()
        {
            var data = HttpContextAccessor!.HttpContext!.Session.GetString(SessionNames.Roles);
            if(data != null) { return; }

            var roles = await RoleBsn!.GetRoleList(0);
            HttpContextAccessor!.HttpContext!.Session.SetString(SessionNames.Roles, JsonConvert.SerializeObject(roles));
        }

        private async Task GetSalaryList()
        {
            var data = HttpContextAccessor!.HttpContext!.Session.GetString(SessionNames.Salaries);
            if (data != null) { return; }
            var salaries = await SalaryBsn!.GetSalaryList(0);
            HttpContextAccessor!.HttpContext!.Session.SetString(SessionNames.Salaries, JsonConvert.SerializeObject(salaries));
        }

        private async Task GetServicePackageList()
        {
            var data = HttpContextAccessor!.HttpContext!.Session.GetString(SessionNames.ServicePackages);
            if (data != null) { return; }
            var servicePackages = await SPBsn!.GetServicePackageList(0);
            HttpContextAccessor!.HttpContext!.Session.SetString(SessionNames.ServicePackages, JsonConvert.SerializeObject(servicePackages));
        }
    }
}
