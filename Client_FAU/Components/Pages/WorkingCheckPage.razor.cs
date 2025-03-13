using Client_FAU.Business.Interfaces;
using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;
using Models;

namespace Client_FAU.Components.Pages
{
    public partial class WorkingCheckPage
    {
        [Inject]
        private WorkingCheck_Int? WKBsn { get; set; }

        private IEnumerable<WorkingCheck>? data = Lists.workingChecks;
        private bool isLoading = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await GetWorkingCheckList();
            StateHasChanged();
        }

        private async Task GetWorkingCheckList()
        {
            if (Lists.workingChecks.Count() > 0) { return; }
            var getWKs = await WKBsn!.GetWorkingCheckList("asc", 0);
            Lists.workingChecks = getWKs;
        }

        async Task ShowLoading()
        {
            isLoading = true;

            await Task.Yield();

            isLoading = false;
        }
    }
}
