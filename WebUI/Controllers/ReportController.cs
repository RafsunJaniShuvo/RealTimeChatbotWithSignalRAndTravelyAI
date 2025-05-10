using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult CurrentStockReport()
        {
            return View();
        }
        public IActionResult DateWiseStockReport()
        {
            return View();
        }
    }
}
