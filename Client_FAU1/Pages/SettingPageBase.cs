using Client_FAU1.Business.Interfaces;
using Client_FAU.Variables;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Client_FAU.Components.Pages
{
    public class SettingPageBase: ComponentBase
    {
        [Inject]
        protected ServicePackage_Int? SPBsn { get; set; }
        [Inject]
        protected IJSRuntime? JSRuntime2 { get; set; }

        protected ServicePackage? Model2 { get; set; } = new();

        protected string message2 = string.Empty;

        protected void ClearForm2() => Model2 = new();

        protected void SetServicePackageProperties()
        {
            Model2!.PackageName = Model2.PackageName!.Trim();
            Model2!.PackageCode = "SP";
            Model2.Price = double.Parse(Model2.GetPrice);
            Model2.AdminUpdate = "AC00000001";
        }

        protected async Task AddServiceDataBase()
        {
            Load.IsLoading  = true;
            try
            {
                SetServicePackageProperties();
                var result = await SPBsn!.AddANewServicePackage(Model2!);

                if (result != null)
                {
                    message2 = "Add new service package successfully";
                    UpdateServicePackagesData(result);
                }
                else
                {
                    message2 = "Add new service package failed. Problems arise !";
                }
            } catch (Exception ex)
            {
                message2 = ex.Message;
            }
            ClearForm2();
            Thread.Sleep(500);
            Load.IsLoading  = false;
            await JSRuntime2!.InvokeVoidAsync("UndisplayServiceSample");
        }

        protected async Task EditServiceDataBase(ServicePackage servicePackage)
        {
            Load.IsLoading  = true;

            if (Lists.servicePackages != null)
            {
                var getSP = Lists.servicePackages.Where(x => x.PackageCode == servicePackage.PackageCode).FirstOrDefault();
                if (getSP != null || getSP != default)
                {
                    if(getSP.IsDeleted != servicePackage.IsDeleted) { }
                    else if (getSP.Price != servicePackage.Price) { }
                    else if (getSP.MemberQuantity != servicePackage.MemberQuantity) { }
                    else if (getSP.NumberOfDays != servicePackage.NumberOfDays) { }
                    else
                    {
                        Load.IsLoading  = false;
                        return;
                    }
                }
            }

            //Else update the service package
            try
            {
                var result = await SPBsn!.EditAnExistServicePackage(servicePackage);
                if (result != null)
                {
                    message2 = $"Edit {result.PackageCode} successfully";
                    UpdateServicePackagesData(result);
                }
                else
                {
                    message2 = "Edit service package failed. Problems arise !";
                }
            }
            catch (Exception ex)
            {
                message2 = ex.Message;
            }
            ClearForm2();
            Thread.Sleep(500);
            Load.IsLoading  = false;
        }

        protected async Task SetDeleteStateForEditDataBase(ServicePackage servicePackage)
        {
            servicePackage.IsDeleted = !servicePackage.IsDeleted;
            await EditServiceDataBase(servicePackage);
        }

        protected void UpdateServicePackagesData(ServicePackage servicePackage)
        {
            if(servicePackage == null) { return; }
            var getSP = Lists.servicePackages!.Where(x => x.PackageCode == servicePackage.PackageCode).FirstOrDefault();
            if(getSP == default || getSP == null)
            {
                Lists.servicePackages.Insert(0, servicePackage);
            }
            else
            {
                int index = Lists.servicePackages.IndexOf(getSP);
                Lists.servicePackages[index] = servicePackage;
            }
        }
    }
}
