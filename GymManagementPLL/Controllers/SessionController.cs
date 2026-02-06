using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
