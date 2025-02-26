using Client_FAU.Business.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Models;
using Client_FAU.Variables;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        private static string sessionName = "branches";
        private string message = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadBranchList();
        }

        private async Task LoadBranchList()
        {
            var data = HttpContextAccessor.HttpContext!.Session.GetString(sessionName);
            if(data != null) 
            {
                branches = JsonConvert.DeserializeObject<List<Branch>>(data)!;
                return;
            }
            var getBranches = await BranchBsn!.GetBranchList(12);
            branches = getBranches;
            HttpContextAccessor.HttpContext!.Session.SetString(sessionName, JsonConvert.SerializeObject(getBranches));
        }

        private void SetAddState()
        {
            ModalState.current_state = ModalState.State.Add;
        }

        private void SetEditState()
        {
            ModalState.current_state = ModalState.State.Edit;
        }

        private void SetModelProperties()
        {
            model.AdminUpdate = "AD00000001";
        }

        private async Task UpdateDatabase()
        {
            try
            {
                SetModelProperties();
                Branch result = new Branch();
                switch (ModalState.current_state)
                {
                    case ModalState.State.Add:
                        result = await BranchBsn!.AddNewBranch(model);
                        break;
                    case ModalState.State.Edit:
                        result = await BranchBsn!.EditAnExistBranch(model);
                        break;
                    default:
                        break;
                }

                if (result != null)
                {
                    message = "Add new Branch Successfully";
                    UpdateData(result);
                }
                else
                {
                    message = "Add new Branch failed. There may be problem !";
                }
            } catch (Exception ex)
            {
                message = ex.Message;
            }
        }

        private void UpdateData(Branch branch)
        {
            if(branches == null) { return; }
            var getBranch = branches.Where(x => x.BranchCode == branch.BranchCode).FirstOrDefault();
            if(getBranch == default || getBranch == null)
            {
                branches.Insert(0, branch);
            } else
            {
                int index = branches.IndexOf(getBranch);
                branches[index] = branch;
            }

            HttpContextAccessor.HttpContext!.Session.SetString(sessionName, JsonConvert.SerializeObject(branches));
        }
    }
}
