using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
