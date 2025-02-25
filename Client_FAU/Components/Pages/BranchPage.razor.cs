using Client_FAU.Business.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Models;

namespace Client_FAU.Components.Pages
{
    public partial class BranchPage
    {
        [Inject]
        public Branch_Int? BranchBsn { get; set; }
        [Inject]
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        private Branch model = new Branch();
        private List<Branch>? branches;
        private static string nameSession = "branches";

        protected override async Task OnInitializedAsync()
        {
            await LoadBranchList();
        }

        private async Task LoadBranchList()
        {
            var data = HttpContextAccessor.HttpContext!.Session.GetString(nameSession);
            if(data != null) 
            {
                branches = JsonConvert.DeserializeObject<List<Branch>>(data)!;
                return;
            }
            var getBranches = await BranchBsn!.GetBranchList(12);
            branches = getBranches;
            HttpContextAccessor.HttpContext!.Session.SetString(nameSession, JsonConvert.SerializeObject(getBranches));
        }

        private async Task UpdateData()
        {
            throw new Exception();
        }
    }
}
