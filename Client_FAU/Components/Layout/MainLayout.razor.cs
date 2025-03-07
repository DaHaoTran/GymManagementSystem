using Blazored.LocalStorage;
using Client_FAU.Business.Interfaces;
using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;
using Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private Account_Int? AccountBsn { get; set; }
        [Inject]
        private Branch_Int? BranchBsn { get; set; }
        [Inject]
        private Equipment_Int? EquipBsn { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetBranchList();
            await GetRoleList();
            await GetSalaryList();
            await GetServicePackageList();
            await GetAccountList();
        }

        private async Task GetRoleList()
        {
            if(Lists.roles.Count() > 0) { return; }
            var getRoles = await RoleBsn!.GetRoleList(0);
            Lists.roles = getRoles;
        }

        private async Task GetSalaryList()
        {
            if (Lists.salaries.Count() > 0) { return; }
            var getSalaries = await SalaryBsn!.GetSalaryList(0);
            Lists.salaries = getSalaries;
        }

        private async Task GetServicePackageList()
        {
            if (Lists.servicePackages.Count() > 0) { return; }
            var getSPs = await SPBsn!.GetServicePackageList(0);
            Lists.servicePackages = getSPs;
        }

        private async Task GetAccountList()
        {
            if (Lists.accounts.Count() > 0) { return; }
            var getAccounts = await AccountBsn!.GetAccountList(9);
            Lists.accounts = getAccounts;
        }

        private async Task GetBranchList()
        {
            if (Lists.branches.Count() > 0) { return; }
            var getBranches = await BranchBsn!.GetBranchList(9);
            Lists.branches = getBranches;
        }

        private async Task GetEquipmentList()
        {
            if(Lists.equipment.Count() > 0) { return; }
            var getEquipment = await EquipBsn!.GetEquipmentList(0);
            Lists.equipment = getEquipment;
        }
    }
}
