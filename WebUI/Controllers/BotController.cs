using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class BotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
