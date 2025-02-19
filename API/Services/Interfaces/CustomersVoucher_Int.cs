using Models;

namespace API.Services.Interfaces
{
    public interface CustomersVoucher_Int
    {
        List<CustomersVoucher> GetCustomersVoucherList();
        CustomersVoucher GetTheCustomersVoucherByOrderNumber(int orderNumber);
        List<CustomersVoucher> GetTheCustomersVouchersByCustomerCode(string customerCode);
        CustomersVoucher AddANewCustomersVoucher(CustomersVoucher customersVoucher);
        CustomersVoucher EditAnExistCustomersVoucher(CustomersVoucher customersVoucher);
        CustomersVoucher DeleteAnExistCustomersVoucher(int orderNumber);
    }
}
