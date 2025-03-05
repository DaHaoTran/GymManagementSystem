using Blazored.LocalStorage;
using Client_FAU.Business.Interfaces;
using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;
using Models;
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
        private ILocalStorageService? LocalStorage { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                await LocalStorage!.ClearAsync();
                await GetRoleList();
                await GetSalaryList();
                await GetServicePackageList();
                StateHasChanged();
            }
        }

        private async Task GetRoleList()
        {
            var data = await LocalStorage!.GetItemAsync<List<Role>>(SessionNames.Roles);
            if(data != null) { return; }

            var roles = await RoleBsn!.GetRoleList(0);
            await LocalStorage.SetItemAsync(SessionNames.Roles, roles);
        }

        private async Task GetSalaryList()
        {
            var data = await LocalStorage!.GetItemAsync<List<Salary>>(SessionNames.Salaries);
            if (data != null) { return; }
            var salaries = await SalaryBsn!.GetSalaryList(0);
            await LocalStorage.SetItemAsync(SessionNames.Salaries, salaries);
        }

        private async Task GetServicePackageList()
        {
            var data = await LocalStorage!.GetItemAsync<List<ServicePackage>>(SessionNames.ServicePackages);
            if (data != null) { return; }
            var servicePackages = await SPBsn!.GetServicePackageList(0);
            await LocalStorage.SetItemAsync(SessionNames.ServicePackages, servicePackages);
        }
    }
}
