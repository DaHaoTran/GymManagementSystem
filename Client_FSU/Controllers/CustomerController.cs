using Client_FSU.Business.Interfaces;
using Client_FSU.Models;
using Client_FSU.Variables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Client_FSU.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Customer_Int _customerBsn;
        private readonly Branch_Int _branchBsn;
        public CustomerController(Customer_Int customerBsn, Branch_Int branchBsn)
        {
            _customerBsn = customerBsn;
            _branchBsn = branchBsn;
        }
        public async Task<IActionResult> Index()
        {
            if(Lists.customers.Count() <= 0) { Lists.customers = await _customerBsn.GetCustomerList(9); }
            return View(Lists.customers);
        }

        public async Task<IActionResult> Create()
        {
            if(Lists.branches.Count() <= 0) { Lists.branches = await _branchBsn.GetBranchList(0); }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CusCustomer customer)
        {
            if (!ModelState.IsValid) { /*var errors = ModelState.Values.SelectMany(v => v.Errors);*/ return View(customer); }
            // Set value
            Customer sCustomer = new Customer();
            sCustomer.CustomerName = customer.CustomerName.Trim();
            sCustomer.PhoneNumber = customer.PhoneNumber.Trim();
            sCustomer.UpdateBy = Validation.StaffCode;
            
            try
            {
                //try get branch code by branch name
                var getBranches = await _branchBsn.GetTheBranchesBySearchString(customer.BranchName!, 1);
                if(getBranches == null) { return View(); }
                sCustomer.BranchCode = getBranches.First().BranchCode;

                //Do business
                var result = await _customerBsn.AddANewCustomer(sCustomer);
                if(result != null)
                {
                    ViewBag.Message = "Create new customer successfully";
                    UpdateCustomerList(result);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Create failed. There are a problem !";
                    return View();  
                }
            } catch (Exception ex)
            {
                var message = ex.Message;
                return View();
            }
        }

        private void UpdateCustomerList(Customer customer)
        {
            var getCustomer = Lists.customers.Where(x => x.PhoneNumber == customer.PhoneNumber).First();
            if (getCustomer == null) { Lists.customers.Insert(0, customer); return; }

            var index = Lists.customers.IndexOf(getCustomer);
            Lists.customers[index] = customer;
        }
    }
}
