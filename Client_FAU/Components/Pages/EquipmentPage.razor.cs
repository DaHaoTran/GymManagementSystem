using Client_FAU.Components.Ingredients;
using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;
using Models;
using Radzen.Blazor;
using System.Collections.Immutable;

namespace Client_FAU.Components.Pages
{
    public partial class EquipmentPage
    {
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
    }
}
