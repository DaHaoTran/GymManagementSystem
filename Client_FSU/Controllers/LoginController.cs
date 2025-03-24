using Client_FSU.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Client_FSU.Variables;
using System.Threading.Tasks;
using Client_FSU.Extensions;

namespace Client_FSU.Controllers
{
    public class LoginController : Controller
    {
        private readonly Account_Int _accountBsn;
        private readonly Token_Int _tokenBsn;
        public LoginController(Account_Int accounBsn, Token_Int tokenBsn)
        {
            _accountBsn = accounBsn;
            _tokenBsn = tokenBsn;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Login login)
        {
            if (!ModelState.IsValid) { return View(); }
            try
            {
                login.Password = !string.IsNullOrEmpty(login.Password) ? PasswordManipulates.EncryptPassword(login.Password) : login.Password;
                var result = await _accountBsn.ValidateAccount(login);
                if (result != null)
                {
                    if (!result.AccountCode.Substring(0, 2).Contains("ST", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewBag.Message = "This account is not allowed to access !";
                        return View();
                    }
                    //Set generate state
                    result.GetRoleName = "string";
                    result.GetSalaryType = "string";

                    Validation.StaffCode = result.AccountCode;
                    Validation.FullName = result.FullName;

                    var token = await _tokenBsn.GenerateJwtToken(result);
                    if (token == null)
                    {
                        ViewBag.Message = "There are unexpected problem. Maybe can try again !";
                        return View();
                    }

                    Validation.Token = token.Trim();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
            
            Validation.IsLoggedIn = true;
            return RedirectToAction("Index", "Customer");
        }
    }
}
