using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Areas.Admin.Controllers
{
    public class DashBoardController :AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
