using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Trainers()
        {
            var Trainers = new List<Trainer>() 
            {
             new Trainer(){Name = "Baher",Phone = "01113411677"},
             new Trainer(){Name = "Asser",Phone = "01556564970"},

            };
            return Json(Trainers);
        }

        public ContentResult Content()
        {
            return Content("Helllo  From iSchool");
        }
    }
}
