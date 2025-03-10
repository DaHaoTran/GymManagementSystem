using Client_FAU.Variables;
using Models;

namespace Client_FAU.Components.Pages
{
    public partial class WorkingCheckPage
    {
        private IEnumerable<WorkingCheck>? data = Lists.workingChecks;
        private bool isLoading = false;

        async Task ShowLoading()
        {
            isLoading = true;

            await Task.Yield();

            isLoading = false;
        }
    }
}
