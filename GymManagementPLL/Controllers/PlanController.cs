using GymManagementBLL.Services.Interfcaes;
using GymManagementBLL.ViewModels.PlanViewModels;
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

        #region Edit.

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id can't be Zero or Negative Number.";
                return RedirectToAction(nameof(Index));
            }
            var Plan = _planService.GetPlanToUpdate(id);
            if (Plan is null)
            {
                TempData["ErrorMessage"] = "Plan is Not Found!";
                return RedirectToAction(nameof(Index));
            }
            return View(Plan);
        }

        [HttpPost]

        public IActionResult Edit([FromRoute] int id , UpdatePlanViewModel updatedPlan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Please correct the errors in the form.");
                return View(updatedPlan);
            }

            bool Result = _planService.UpdatePlan(id, updatedPlan);

            if (Result) 
            {
                TempData["SuccessMessage"] = "Plan is Updated Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Plan is Failed To Update.";
            }
            return RedirectToAction(nameof(Index));
        }



        #endregion


        #region Active & Deactive - Soft Delete.

        [HttpPost]

        public IActionResult Activate(int id)
        {
            var Result = _planService.ToggleStatus(id);
            if (Result) 
            {
                TempData["SuccessMessage"] = "Plan Status Changed Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Change Plan Status. Please try again.";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion


    }
}
        
    
