using Client_FAU.Business.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Models;
using Client_FAU.Variables;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Blazored.LocalStorage;

namespace Client_FAU.Components.Pages
{
    public partial class BranchPage
    {
        [Inject]
        private Branch_Int? BranchBsn { get; set; }
        [SupplyParameterFromForm]
        private Branch? Model { get; set; } = new();

        private string message = string.Empty;

        private void ClearForm() => Model = new();

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
            Model!.BranchCode = "BR";
            Model!.AdminUpdate = "AD00000001";
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
                        result = await BranchBsn!.AddNewBranch(Model!);
                        break;
                    case ModalState.State.Edit:
                        result = await BranchBsn!.EditAnExistBranch(Model!);
                        break;
                    default:
                        break;
                }

                if (result != null)
                {
                    message = "Add new Branch Successfully";
                    UpdateBranchData(result);
                }
                else
                {
                    message = "Add new Branch failed. There may be problem !";
                }
            } catch (Exception ex)
            {
                message = ex.Message;
            }

            ClearForm();
        }

        private void UpdateBranchData(Branch branch)
        {
            if(Lists.branches == null) { return; }
            var getBranch = Lists.branches.Where(x => x.BranchCode == branch.BranchCode).FirstOrDefault();
            if(getBranch == default || getBranch == null)
            {
                Lists.branches.Insert(0, branch);
            } else
            {
                int index = Lists.branches.IndexOf(getBranch);
                Lists.branches[index] = branch;
            }
        }

        private void ShowInvalidMessage()
        {
            message = "The inputs are invalid, Check again !";
        }
    }
}
