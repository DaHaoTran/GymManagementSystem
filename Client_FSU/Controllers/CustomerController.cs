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
        private static string customerCode = string.Empty;
        public CustomerController(Customer_Int customerBsn, Branch_Int branchBsn)
        {
            _customerBsn = customerBsn;
            _branchBsn = branchBsn;
        }
        public async Task<IActionResult> Index()
        {
            if(Lists.customers.Count() <= 0) { Lists.customers = await _customerBsn.GetCustomerList(9); }
            ViewBag.CustomerCode = !string.IsNullOrEmpty(customerCode) ? customerCode : string.Empty;
            return View(Lists.customers);
        }

        public async Task<IActionResult> Create()
        {
            if(Lists.branches.Count() <= 0) { Lists.branches = await _branchBsn.GetBranchList(0); }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            //if (!ModelState.IsValid) { /*var errors = ModelState.Values.SelectMany(v => v.Errors);*/ return View(customer); }    
            try
            {
                //try get branch code by branch name
                var getBranches = await _branchBsn.GetTheBranchesBySearchString(customer.BranchName!, 1);
                if(getBranches == null) { return View(); }
                // Set value
                customer.CustomerCode = "CT";
                customer.CustomerName = customer.CustomerName.Trim();
                customer.PhoneNumber = customer.PhoneNumber.Trim();
                customer.UpdateBy = Validation.StaffCode;
                customer.BranchCode = getBranches.First().BranchCode;

                //Do business
                var result = await _customerBsn.AddANewCustomer(customer);
                if(result != null)
                {
                    ViewBag.Message = "Create new customer successfully";
                    customerCode = result.CustomerCode;
                    UpdateCustomerList(result);
                    return RedirectToAction("Index", "Customer");
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
            var getCustomer = Lists.customers.Where(x => x.PhoneNumber == customer.PhoneNumber).FirstOrDefault();
            if (getCustomer == default) { Lists.customers.Insert(0, customer); return; }

            var index = Lists.customers.IndexOf(getCustomer);
            Lists.customers[index] = customer;
        }

        public async Task<IActionResult> Details(string id)
        {
            var getCustomer = Lists.customers.Where(x => x.CustomerCode == id).FirstOrDefault();
            if (getCustomer == default) { ViewBag.Message = "Problem arise !"; }
            var getBranch = await _branchBsn.GetTheBranchByBranchCode(getCustomer!.BranchCode);
            if(getBranch != null) { getCustomer.BranchName = getBranch.BranchName; }
            return View(getCustomer);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (Lists.branches.Count() <= 0) { Lists.branches = await _branchBsn.GetBranchList(0); }
            var getCustomer = new Customer();
            getCustomer = Lists.customers.Where(x => x.CustomerCode == id).FirstOrDefault();
            if(getCustomer == default) { ViewBag.Message = "Problem arise !"; }

            var getBranch = await _branchBsn.GetTheBranchByBranchCode(getCustomer!.BranchCode);
            if(getBranch == null) { ViewBag.Message = "Problem with load branch name !"; return View(getCustomer);  }
            getCustomer.BranchName = getBranch!.BranchName;

            return View(getCustomer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            //if (!ModelState.IsValid) { /*var errors = ModelState.Values.SelectMany(v => v.Errors);*/ return View(customer); }
            try
            {
                //try get branch code by branch name
                var getBranches = await _branchBsn.GetTheBranchesBySearchString(customer.BranchName!, 1);
                if (getBranches == null) { return View(); }
                //Set value
                customer.CustomerName = customer.CustomerName.Trim();
                customer.PhoneNumber = customer.PhoneNumber.Trim();
                customer.UpdateBy = Validation.StaffCode;
                customer.BranchCode = getBranches.First().BranchCode;

                //Do business
                var result = await _customerBsn.EditAnExistCustomer(customer);
                if (result != null)
                {
                    ViewBag.Message = $"Edit customer {customer.CustomerCode} successfully";
                    customerCode = result.CustomerCode; 
                    UpdateCustomerList(result);
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ViewBag.Message = "Edit failed. There are a problem !";
                    return View();
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return View();
            }
        }
    }
}
