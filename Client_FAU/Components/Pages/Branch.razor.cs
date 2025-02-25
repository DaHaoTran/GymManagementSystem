using Client_FAU.Business.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Client_FAU.Components.Pages
{
    public partial class Branch
    {
        [Inject]
        public Branch_Int? branchBsn { get; set; }
        [Inject]
        public IHttpContextAccessor httpContextAccessor { get; set; }

        private List<Branch>? branches;
        private static string nameSession = "branches";
        
        protected override async Task OnInitializedAsync()
        {
            await LoadBranchList();
        }

        private async Task LoadBranchList()
        {
            var data = httpContextAccessor.HttpContext!.Session.GetString(nameSession);
            if(data != null) 
            {
                branches = JsonConvert.DeserializeObject<List<Branch>>(data)!;
                return;
            }
            var getBranches = await branchBsn!.GetBranchList(12);
            branches = getBranches;
            httpContextAccessor.HttpContext!.Session.SetString(nameSession, JsonConvert.SerializeObject(getBranches));
        }
    }
}
