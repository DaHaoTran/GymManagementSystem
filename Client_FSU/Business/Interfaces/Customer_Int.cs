using Models;

namespace Client_FSU.Business.Interfaces
{
    public interface Customer_Int
    {
        Task<List<Customer>> GetCustomerList(int limit);
        Task<Customer> GetTheCustomerByCustomerCode(string customerCode);
        Task<List<Customer>> GetTheCustomersBySearchString(string str, int limit);
        Task<List<Customer>> GetTheCustomersByBranchCode(string branchCode, int limit);
        Task<Customer> AddANewCustomer(Customer customer);
        Task<Customer> EditAnExistCustomer(Customer customer);  
        Task<Customer> DeleteAnExistCustomer(string customerCode);
    }
}
