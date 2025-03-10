using Client_FAU.Business.Interfaces;
using Client_FAU.Components.Ingredients;
using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models;

namespace Client_FAU.Components.Pages
{
    public partial class FinePage
    {
        [Inject]
        private Fine_Int? FineBsn {  get; set; }
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }

        private IEnumerable<Fine>? data = Lists.fines;
        private bool isLoading = false;

        async Task ShowLoading()
        {
            isLoading = true;

            await Task.Yield();

            isLoading = false;
        }

        private async Task EditFineDatabase(Fine fine)
        {
            try
            {
                var result = await FineBsn!.EditAnExistFine(fine);

                if (result != null)
                {
                    await JSRuntime!.InvokeVoidAsync("PlaySuccessAudio");
                    UpdateFinesData(result);
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

        private void UpdateFinesData(Fine fine)
        {
            if (fine == null) { return; }
            var getFine = Lists.fines!.Where(x => x.FineCode == fine.FineCode).FirstOrDefault();
            if (getFine == default || getFine == null)
            {
                Lists.fines!.Insert(0, fine);
            }
            else
            {
                var index = Lists.fines!.IndexOf(getFine);
                Lists.fines[index] = fine;
            }
        }
    }
}
