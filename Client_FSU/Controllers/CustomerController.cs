using Microsoft.AspNetCore.Mvc;

namespace Client_FSU.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
