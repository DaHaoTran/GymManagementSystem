using Models;

namespace API.Services.Interfaces
{
    public interface CustomersVoucher_Int
    {
        Task<List<CustomersVoucher>> GetCustomersVoucherList();
        Task<CustomersVoucher> GetTheCustomersVoucherByOrderNumber(int orderNumber);
        Task<List<CustomersVoucher>> GetTheCustomersVouchersByCustomerCode(string customerCode);
        Task<CustomersVoucher> AddANewCustomersVoucher(CustomersVoucher customersVoucher);
        Task<CustomersVoucher> EditAnExistCustomersVoucher(CustomersVoucher customersVoucher);
        Task<CustomersVoucher> DeleteAnExistCustomersVoucher(int orderNumber);
    }
}
