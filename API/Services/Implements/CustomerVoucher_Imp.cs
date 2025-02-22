using API.Services.Interfaces;
using DBA.Context;
using Microsoft.EntityFrameworkCore;
using Models;

namespace API.Services.Implements
{
    public class CustomerVoucher_Imp : CustomersVoucher_Int
    {
        private GymManagementSystemDBContext _dBContext;
        public CustomerVoucher_Imp(GymManagementSystemDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<CustomersVoucher> AddANewCustomersVoucher(CustomersVoucher customersVoucher)
        {
            await _dBContext.CustomersVouchers.AddAsync(customersVoucher);
            await _dBContext.SaveChangesAsync();
            return customersVoucher;
        }

        public async Task<CustomersVoucher> DeleteAnExistCustomersVoucher(int orderNumber)
        {
            var getCV = await GetTheCustomersVoucherByOrderNumber(orderNumber);
            if(getCV == null) { return getCV; }

            _dBContext.CustomersVouchers.Remove(getCV);
            await _dBContext.SaveChangesAsync();

            return getCV;
        }

        public async Task<CustomersVoucher> EditAnExistCustomersVoucher(CustomersVoucher customersVoucher)
        {
            var getCV = await GetTheCustomersVoucherByOrderNumber(customersVoucher.OrderNumber);
            if(getCV == null) { return getCV; }

            getCV.CustomerCode = customersVoucher.CustomerCode;
            getCV.PackageCode = customersVoucher.PackageCode;
            getCV.CreateDate = customersVoucher.CreateDate;
            getCV.EndDate = customersVoucher.EndDate;
            getCV.IsDeleted = customersVoucher.IsDeleted;
            getCV.UpdateBy = customersVoucher.UpdateBy;

            await _dBContext.SaveChangesAsync();

            return getCV;
        }

        public async Task<List<CustomersVoucher>> GetCustomersVoucherList()
        {
            return await _dBContext.CustomersVouchers.ToListAsync();
        }

        public async Task<CustomersVoucher> GetTheCustomersVoucherByOrderNumber(int orderNumber)
        {
            return await _dBContext.CustomersVouchers.Where(x => x.OrderNumber == orderNumber).FirstOrDefaultAsync();
        }

        public async Task<List<CustomersVoucher>> GetTheCustomersVouchersByCustomerCode(string customerCode)
        {
            return await _dBContext.CustomersVouchers.Where(x => x.CustomerCode == customerCode).ToListAsync();
        }
    }
}
