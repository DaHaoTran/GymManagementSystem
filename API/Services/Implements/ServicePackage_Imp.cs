using API.Services.Interfaces;
using DBA.Context;
using Microsoft.EntityFrameworkCore;
using Models;

namespace API.Services.Implements
{
    public class ServicePackage_Imp : ServicePackage_Int
    {
        private readonly GymManagementSystemDBContext _dBContext;
        public ServicePackage_Imp(GymManagementSystemDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<ServicePackage> AddANewServicePackage(ServicePackage servicePackage)
        {
            await _dBContext.ServicePackages.AddAsync(servicePackage);
            await _dBContext.SaveChangesAsync();
            return servicePackage;
        }

        public async Task<ServicePackage> DeleteAnExistServicePackage(string packageCode)
        {
            var getSP = await GetTheServicePackageByPackageCode(packageCode);
            if(getSP == null) { return getSP; }

            _dBContext.ServicePackages.Remove(getSP);
            await _dBContext.SaveChangesAsync();

            return getSP;
        }

        public async Task<ServicePackage> EditAnExistServicePackage(ServicePackage servicePackage)
        {
            var getSP = await GetTheServicePackageByPackageCode(servicePackage.PackageCode);
            if(getSP == null) { return getSP; }

            getSP.PackageName = servicePackage.PackageName;
            getSP.Price = servicePackage.Price;
            getSP.MemberQuantity = servicePackage.MemberQuantity;
            getSP.NumberOfDays = servicePackage.NumberOfDays;
            getSP.IsDeleted = servicePackage.IsDeleted;
            getSP.AdminUpdate = servicePackage.AdminUpdate;

            await _dBContext.SaveChangesAsync();

            return getSP;
        }

        public async Task<List<ServicePackage>> GetServicePackageList()
        {
            return await _dBContext.ServicePackages.ToListAsync();
        }

        public async Task<ServicePackage> GetTheServicePackageByPackageCode(string packageCode)
        {
            return await _dBContext.ServicePackages.Where(x => x.PackageCode == packageCode).FirstOrDefaultAsync();
        }

        public async Task<List<ServicePackage>> GetTheServicePackagesByPackageName(string packageName)
        {
            return await _dBContext.ServicePackages.Where(x => x.PackageName.Contains(packageName, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }
    }
}
