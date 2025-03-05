using Models;

namespace API.Services.Interfaces
{
    public interface ServicePackage_Int
    {
        Task<List<ServicePackage>> GetServicePackageList(int limit);
        Task<ServicePackage> GetTheServicePackageByPackageCode(string packageCode);
        Task<List<ServicePackage>> GetTheServicePackagesByPackageName(string packageName);
        Task<ServicePackage> AddANewServicePackage(ServicePackage servicePackage);
        Task<ServicePackage> EditAnExistServicePackage(ServicePackage servicePackage);
        Task<ServicePackage> DeleteAnExistServicePackage(string packageCode);
    }
}
