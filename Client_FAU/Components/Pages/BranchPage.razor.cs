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
using Microsoft.JSInterop;

namespace Client_FAU.Components.Pages
{
    public partial class BranchPage
    {
        [Inject]
        private Branch_Int? BranchBsn { get; set; }
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        [SupplyParameterFromForm]
        private Branch? Model { get; set; } = new();

        private string str = string.Empty;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await GetBranchList();
            StateHasChanged();
        }

        private async Task GetBranchList()
        {
            if (Lists.branches.Count() > 0) { return; }
            var getBranches = await BranchBsn!.GetBranchList(9);
            Lists.branches = getBranches;
        }

        private void ClearForm() => Model = new();

        private void SetModelState(ModalState.State state)
        {
            ModalState.current_state = state;
        }

        private void SetAddState()
        {
            Model = new Branch();
            SetModelState(ModalState.State.Add);
        }

        private void SetEditState(Branch branch)
        {
            Model!.BranchCode = branch.BranchCode;
            Model!.BranchName = branch.BranchName;
            Model.Address = branch.Address;
            Model.QuantityOfPTs = branch.QuantityOfPTs;
            Model.QuantityOfStaffs = branch.QuantityOfStaffs;
            Model.QuantityOfWorkers = branch.QuantityOfWorkers;
            Model.AdminUpdate = branch.AdminUpdate;
            Model.IsDeleted = branch.IsDeleted;
            SetModelState(ModalState.State.Edit);
        }

        private void SetModelProperties()
        {
            Model!.BranchCode = "BR";
            Model!.AdminUpdate = "AD00000001";
            Model.BranchName = Model.BranchName.Trim();
            Model.Address = Model.Address.Trim();
        }

        private void SetModelProperties2()
        {
            Model!.AdminUpdate = "AD00000001";
            Model.BranchName = Model.BranchName.Trim();
            Model.Address = Model.Address.Trim();
        }

        private async Task AddNewBranchDatabase()
        {
            Load.IsLoading = true;
            try
            {
                SetModelProperties();
                var result = await BranchBsn!.AddNewBranch(Model!);

                if (result != null)
                {
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateBranchesData(result);
                }
                else
                {
                    await JSRuntime!.InvokeVoidAsync("PlayErrorAudio");
                }
            } catch (Exception ex)
            {
                Notification.message = ex.Message;
            }
            ClearForm();
            Thread.Sleep(500);
            Load.IsLoading = false;
            await JSRuntime!.InvokeVoidAsync("CloseEditModal");

            Thread.Sleep(100);
            await JSRuntime!.InvokeVoidAsync("Reload");
        }

        private async Task EditBranchDatabase()
        {
            Load.IsLoading = true;
            try
            {
                SetModelProperties2();
                var result = await BranchBsn!.EditAnExistBranch(Model!);

                if (result != null)
                {
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateBranchesData(result);
                }
                else
                {
                    await JSRuntime!.InvokeVoidAsync("PlayErrorAudio");
                }
            } catch(Exception ex)
            {
                Notification.message = ex.Message;
            }
            ClearForm();
            Thread.Sleep(500);
            Load.IsLoading = false;
            await JSRuntime!.InvokeVoidAsync("CloseEditModal");

            Thread.Sleep(100);
            await JSRuntime!.InvokeVoidAsync("Reload");
        }

        private async Task EditBranchStateDatabase(Branch branch)
        {
            branch.IsDeleted = !branch.IsDeleted;
            Load.IsLoading = true;
            try
            {
                var result = await BranchBsn!.EditAnExistBranch(branch);
                if(result != null)
                {
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateBranchesData(result);
                } else
                {
                    await JSRuntime!.InvokeVoidAsync("PlayErrorAudio");
                }
            } catch (Exception ex)
            {
                Notification.message = ex.Message;
            }
            ClearForm();
            Thread.Sleep(500);
            Load.IsLoading = false;

            Thread.Sleep(100);
            await JSRuntime!.InvokeVoidAsync("Reload");
        }

        private async Task UpdateAccountDatabase()
        {
            switch(ModalState.current_state)
            {
                case ModalState.State.Add:
                    await AddNewBranchDatabase();
                    break;
                case ModalState.State.Edit:
                    await EditBranchDatabase(); 
                    break;
                default:
                    break;
            }
        }

        private async Task FindBranchesInDatabase()
        {
            if (string.IsNullOrEmpty(str)) { return; }
            Load.IsLoading = true;
            try
            {
                var result = await BranchBsn!.GetTheBranchesBySearchString(str.Trim(), 9);
                if(result != null)
                {
                    Lists.branches = result;
                } else
                {
                    Lists.branches = new List<Branch>();
                }
            } catch(Exception ex)
            {
                Notification.message = ex.Message;
            }
            Load.IsLoading = false;

            Thread.Sleep(100);
            await JSRuntime!.InvokeVoidAsync("Reload");
        }

        private void UpdateBranchesData(Branch branch)
        {
            if(Lists.branches == null) { return; }
            var getBranch = Lists.branches.Where(x => x.BranchCode == branch.BranchCode).FirstOrDefault();
            if(getBranch == default || getBranch == null)
            {
                Lists.branches.Insert(0, branch);
                if(Lists.branches.Count() >= 9) { Lists.branches.RemoveAt(Lists.branches.Count() - 1); }
            } else
            {
                int index = Lists.branches.IndexOf(getBranch);
                Lists.branches[index] = branch;
            }
        }

        private void ShowInvalidMessage()
        {
            Notification.message = "The inputs are invalid, Check again !";
        }
    }
}
