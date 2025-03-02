using Client_FAU.Business.Interfaces;
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
        protected IHttpContextAccessor? HttpContextAccessor2 { get; set; }
        [Inject]
        protected IJSRuntime? JSRuntime2 { get; set; }
        [SupplyParameterFromForm]
        protected ServicePackage? Model2 { get; set; } = new();

        protected List<ServicePackage>? servicePackages;
        protected string sessionName2 = "servicePackages";
        protected bool isLoading2 = false;
        protected string message2 = string.Empty;

        protected async Task LoadServicePackageList()
        {
            var data = HttpContextAccessor2!.HttpContext!.Session.GetString(sessionName2);
            if (data != null)
            {
                servicePackages = JsonConvert.DeserializeObject<List<ServicePackage>>(data);
                return;
            }
            var getServicePackages = await SPBsn!.GetServicePackageList(12);
            servicePackages = getServicePackages;
            HttpContextAccessor2!.HttpContext!.Session.SetString(sessionName2, JsonConvert.SerializeObject(getServicePackages));
        }

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
            isLoading2 = true;
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
            isLoading2 = false;
            await JSRuntime2!.InvokeVoidAsync("UndisplayServiceSample");
        }

        protected async Task EditServiceDataBase(ServicePackage servicePackage)
        {
            isLoading2 = true;

            //Get the service packages from the session and check if the service package is not null and has changed
            var sessionSPs = HttpContextAccessor2!.HttpContext!.Session.GetString(sessionName2);
            if (sessionSPs != null)
            {
                var temSPs = JsonConvert.DeserializeObject<List<ServicePackage>>(sessionSPs);
                var getSP = temSPs!.Where(x => x.PackageCode == servicePackage.PackageCode).FirstOrDefault();
                if (getSP != null || getSP != default)
                {
                    if (getSP.Price != servicePackage.Price) { }
                    else if (getSP.MemberQuantity != servicePackage.MemberQuantity) { }
                    else if (getSP.NumberOfDays != servicePackage.NumberOfDays) { }
                    else
                    {
                        isLoading2 = false;
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
            isLoading2 = false;
        }

        protected void UpdateServicePackagesData(ServicePackage servicePackage)
        {
            if(servicePackage == null) { return; }
            var getSP = servicePackages!.Where(x => x.PackageCode == servicePackage.PackageCode).FirstOrDefault();
            if(getSP == default || getSP == null)
            {
                servicePackages!.Insert(0, servicePackage);
            }
            else
            {
                int index = servicePackages!.IndexOf(getSP);
                servicePackages[index] = servicePackage;
            }

            HttpContextAccessor2!.HttpContext!.Session.SetString(sessionName2, JsonConvert.SerializeObject(servicePackages));
        }
    }
}
