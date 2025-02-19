using Models;

namespace API.Services.Interfaces
{
    public interface Customer_Int
    {
        List<Customer> GetCustomerList();
        Customer GetTheCustomerByCustomerCode(string customerCode);
        List<Customer> GetTheCustomersByCustomerName(string customerName);
        Customer GetTheCustomerByPhoneNumber(string phoneNumber);
        List<Customer> GetTheCustomersByBranchCode(string branchCode);
        Customer AddANewCustomer(Customer customer);
        Customer EditAnExistCustomer(Customer customer);
        Customer DeleteAnExistCustomer(string customerCode);
    }
}
