using Models;

namespace API.Services.Interfaces
{
    public interface Customer_Int
    {
        Task<List<Customer>> GetCustomerList(int limit);
        Task<Customer> GetTheCustomerByCustomerCode(string customerCode);
        Task<List<Customer>> GetTheCustomersByCustomerName(string customerName);
        Task<List<Customer>> GetTheCustomerByPhoneNumber(string phoneNumber);
        Task<List<Customer>> GetTheCustomersByBranchCode(string branchCode);
        Task<Customer> AddANewCustomer(Customer customer);
        Task<Customer> EditAnExistCustomer(Customer customer);
        Task<Customer> DeleteAnExistCustomer(string customerCode);
    }
}
