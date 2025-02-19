using Models;

namespace API.Services.Interfaces
{
    public interface ServicePackage_Int
    {
        List<ServicePackage> GetServicePackageList();
        ServicePackage GetTheServicePackageByPackageCode(string packageCode);
        List<ServicePackage> GetTheServicePackagesByPackageName(string packageName);
        ServicePackage AddANewServicePackage(ServicePackage servicePackage);
        ServicePackage EditAnExistServicePackage(ServicePackage servicePackage);
        ServicePackage DeleteAnExistServicePackage(string packageCode);
    }
}
