using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
