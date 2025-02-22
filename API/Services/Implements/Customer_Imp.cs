using API.Services.Interfaces;
using DBA.Context;
using Microsoft.EntityFrameworkCore;
using Models;

namespace API.Services.Implements
{
    public class Customer_Imp : Customer_Int
    {
        private readonly GymManagementSystemDBContext _dBContext;
        public async Task<Customer> AddANewCustomer(Customer customer)
        {
            await _dBContext.Customers.AddAsync(customer);
            await _dBContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> DeleteAnExistCustomer(string customerCode)
        {
            var getCustomer = await GetTheCustomerByCustomerCode(customerCode);
            if(getCustomer == null) { return getCustomer; }

            _dBContext.Customers.Remove(getCustomer);
            await _dBContext.SaveChangesAsync();

            return getCustomer;
        }

        public async Task<Customer> EditAnExistCustomer(Customer customer)
        {
            var getCustomer = await GetTheCustomerByCustomerCode(customer.CustomerCode);
            if(getCustomer == null) { return getCustomer; }

            getCustomer.CustomerName = customer.CustomerName;
            getCustomer.PhoneNumber = customer.PhoneNumber;
            getCustomer.IsBanned = customer.IsBanned;
            getCustomer.BannedReason = customer.BannedReason;
            getCustomer.BranchCode = customer.BranchCode;
            getCustomer.UpdateBy = customer.UpdateBy;

            await _dBContext.SaveChangesAsync();

            return getCustomer;
        }

        public async Task<List<Customer>> GetCustomerList()
        {
            return await _dBContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetTheCustomerByCustomerCode(string customerCode)
        {
            return await _dBContext.Customers.Where(x => x.CustomerCode == customerCode).FirstOrDefaultAsync();
        }

        public async Task<List<Customer>> GetTheCustomerByPhoneNumber(string phoneNumber)
        {
            return await _dBContext.Customers.Where(x => x.PhoneNumber == phoneNumber).ToListAsync();
        }

        public async Task<List<Customer>> GetTheCustomersByBranchCode(string branchCode)
        {
            return await _dBContext.Customers.Where(x => x.BranchCode == branchCode).ToListAsync();
        }

        public async Task<List<Customer>> GetTheCustomersByCustomerName(string customerName)
        {
            return await _dBContext.Customers.Where(x => x.CustomerName.Contains(customerName, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }
    }
}
