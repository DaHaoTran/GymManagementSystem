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

        async Task ShowLoading()
        {
            isLoading = true;

            await Task.Yield();

            isLoading = false;
        }

        private void SetAddState()
        {
            ModalState.current_state = ModalState.State.Add;
        }

        private void SetEquipmentProperties()
        {
            int count = Lists.equipment.Count() + 1;
            Model!.EquipCode = "EQ" + "00000000".Substring(0,8 - count.ToString().Length) + count.ToString();
            Model.EquipName = Model.EquipName.Trim();
            Model.Status = Model.Status.Trim();
            Model.Note = Model.Status.Trim();
            Model.AdminUpdate = "AD00000001";
            Model.IsDeleted = false;
            Model.IsReceived = false;
        }

        private void ClearForm() => Model = new();

        private async Task AddEquipmentDatabase()
        {
            Load.IsLoading = true;
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
            Thread.Sleep(500);
            Load.IsLoading = false;

            Thread.Sleep(100);
            await JSRuntime!.InvokeVoidAsync("Reload");
        }

        private async Task UpdateEquipmentDataBase()
        {
            switch (ModalState.current_state)
            {
                case ModalState.State.Add:
                    await AddEquipmentDatabase();
                    break;
                //case ModalState.State.Edit:
                //    await EditAccountDataBase();
                //    break;
                default:
                    break;
            }
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
                var index = Lists.equipment.IndexOf(equipment);
                Lists.equipment[index] = equipment;
            }
            
            data = Lists.equipment;
        }
    }
}
