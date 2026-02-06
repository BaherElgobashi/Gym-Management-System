using GymManagementBLL.Services.Interfcaes;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        #region Get All Plans.

        // Get All Plans.

        public IActionResult Index()
        {
            var Plans = _planService.GetAllPlans();

            return View(Plans);
        }



        #endregion

        #region Get Plan Details.

        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id can't be Zero or Negative Number.";

                return RedirectToAction(nameof(Index));
            }

            var Plan = _planService.GetPlanById(id);
            if (Plan == null)
            {
                TempData["ErrorMessage"] = "Plan is Not Found.";
                return RedirectToAction(nameof(Index));

            }
            return View(Plan);




        }
        #endregion



    }
}
        
    
