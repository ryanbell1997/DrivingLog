using Microsoft.AspNetCore.Mvc;

namespace DrivingLog.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
