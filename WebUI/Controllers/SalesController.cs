using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
