using Models;

namespace Client_FAU.Business.Interfaces
{
    public interface ServicePackage_Int
    {
        Task<List<ServicePackage>> GetServicePackageList(int limit);
        Task<ServicePackage> GetTheServicePackageByPackageCode(string packageCode);
        Task<List<ServicePackage>> GetTheServicePackagesBySearchString(string str, int limit);
        Task<ServicePackage> AddANewServicePackage(ServicePackage servicePackage);
        Task<ServicePackage> EditAnExistServicePackage(ServicePackage servicePackage);
        Task<ServicePackage> DeleteAnExistServicePackage(string packageCode);
    }
}
