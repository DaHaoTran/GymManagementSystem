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
        private HttpContextAccessor? HttpContextAccessor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetRoleList();
        }

        private async Task GetRoleList()
        {
            var data = HttpContextAccessor!.HttpContext!.Session.GetString(SessionNames.Roles);
            if(data != null) { return; }

            var roles = await RoleBsn!.GetRoleList(0);
            HttpContextAccessor!.HttpContext!.Session.SetString(SessionNames.Roles, JsonConvert.SerializeObject(roles));
        }
    }
}
