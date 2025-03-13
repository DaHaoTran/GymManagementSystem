using Client_FAU.Business.Implements;
using Client_FAU.Business.Interfaces;
using Client_FAU.Components.Ingredients;
using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models;
using Radzen.Blazor;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Client_FAU.Components.Pages
{
    public partial class EquipmentPage
    {
        [Inject]
        private Equipment_Int? EquipBsn { get; set; }
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        [SupplyParameterFromForm]
        private Equipment? Model { get; set; } = new();

        private IEnumerable<Equipment>? data= (IEnumerable<Equipment>)Lists.equipment;
        private IEnumerable<Branch>? _branches = (IEnumerable<Branch>?)Lists.branches;
        private bool isLoading = false;
        private string sip = string.Empty;
        private RadzenDropDown<string>? radzenDropDown;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await GetEquipmentList();
            StateHasChanged();
        }
        private async Task GetEquipmentList()
        {
            if (Lists.equipment.Count() > 0) { return; }
            var getEquipment = await EquipBsn!.GetEquipmentList(0);
            Lists.equipment = getEquipment;
        }

        async Task ShowLoading()
        {
            isLoading = true;

            await Task.Yield();

            isLoading = false;
        }

        private void SetAddState()
        {
            Model = new();
            ModalState.current_state = ModalState.State.Add;
        }


        private void SetEditState(Equipment equipment)
        {
            Model!.EquipCode = equipment.EquipCode;
            Model.BranchCode = equipment.BranchCode;
            Model.EquipName = equipment.EquipName;
            Model.Status = equipment.Status;
            Model.Note = equipment.Note;
            Model.AdminUpdate = equipment.AdminUpdate;
            Model.StaffUpdate = equipment.StaffUpdate;
            Model.IsDeleted = equipment.IsDeleted;
            Model.IsReceived = equipment.IsReceived;
            ModalState.current_state = ModalState.State.Edit;
        }

        private void SetEquipmentProperties()
        {
            int count = Lists.equipment.Count() + 1;
            Model!.EquipCode = "EQ" + "00000000".Substring(0,8 - count.ToString().Length) + count.ToString();
            Model.EquipName = Model.EquipName.Trim();
            Model.Status = !string.IsNullOrEmpty(Model.Status) ? Model.Status.Trim() : Model.Status;
            Model.Note = !string.IsNullOrEmpty(Model.Note) ? Model.Note!.Trim() : Model.Note;
            Model.AdminUpdate = Validation.AccountCode;
            Model.IsDeleted = false;
            Model.IsReceived = false;
        }

        private void SetEquipmentProperties2()
        {
            Model!.EquipName = Model.EquipName.Trim();
            Model.Status = !string.IsNullOrEmpty(Model.Status) ? Model.Status.Trim() : Model.Status;
            Model.Note = !string.IsNullOrEmpty(Model.Note) ? Model.Note!.Trim() : Model.Note;
            Model.AdminUpdate = Validation.AccountCode;
        }

        private void ClearForm() => Model = new();

        private async Task AddEquipmentDatabase()
        {
            try
            {
                SetEquipmentProperties();
                var result = await EquipBsn!.AddANewEquipment(Model!);
                if (result != null)
                {
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateEquipmentData(result);
                }
                else
                {
                    await JSRuntime!.InvokeVoidAsync("PlayErrorAudio");
                }
            }
            catch (Exception ex)
            {
                Notification.message = ex.Message;
            }
            ClearForm();

            Thread.Sleep(600);
            await JSRuntime!.InvokeVoidAsync("Reload");
        }

        private async Task EditEquipmentDatabase()
        {
            try
            {
                SetEquipmentProperties2();
                var result = await EquipBsn!.EditAnExistEquipment(Model!);
                if (result != null)
                {
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateEquipmentData(result);
                }
                else
                {
                    await JSRuntime!.InvokeVoidAsync("PlayErrorAudio");
                }
            }
            catch (Exception ex)
            {
                Notification.message = ex.Message;
            }
            ClearForm();

            Thread.Sleep(600);
            await JSRuntime!.InvokeVoidAsync("Reload");
        }

        private async Task UpdateEquipmentDataBase()
        {
            switch (ModalState.current_state)
            {
                case ModalState.State.Add:
                    await AddEquipmentDatabase();
                    break;
                case ModalState.State.Edit:
                    await EditEquipmentDatabase();
                    break;
                default:
                    break;
            }
        }

        private async Task EditEquipmentDeletedStateDatabase(Equipment equipment)
        {
            //equipment.IsDeleted = !equipment.IsDeleted;
            try
            {
                var result = await EquipBsn!.EditAnExistEquipment(equipment);
                if (result != null)
                {
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateEquipmentData(result);
                }
                else
                {
                    await JSRuntime!.InvokeVoidAsync("PlayErrorAudio");
                }
            }
            catch (Exception ex)
            {
                Notification.message = ex.Message;
            }

            Thread.Sleep(600);
            await JSRuntime!.InvokeVoidAsync("Reload");

        }

        private async Task EditEquipmentRecievedStateDatabase(Equipment equipment)
        {
            //equipment.IsReceived = !equipment.IsReceived;
            try
            {
                var result = await EquipBsn!.EditAnExistEquipment(equipment);
                if (result != null)
                {
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateEquipmentData(result);
                }
                else
                {
                    await JSRuntime!.InvokeVoidAsync("PlayErrorAudio");
                }
            }
            catch (Exception ex)
            {
                Notification.message = ex.Message;
            }

            Thread.Sleep(600);
            await JSRuntime!.InvokeVoidAsync("Reload");

        }

        private void UpdateEquipmentData(Equipment equipment)
        {
            if (equipment == null) { return; }
            var getEquipment = Lists.equipment.Where(x => x.EquipCode == equipment.EquipCode).FirstOrDefault();
            if (getEquipment == null || getEquipment == default)
            {
                Lists.equipment.Insert(0, equipment);
            } else
            {
                var index = Lists.equipment.IndexOf(getEquipment);
                Lists.equipment[index] = equipment;
            }
            
            data = Lists.equipment;
        }
    }
}
