using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberShipViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class MembershipController : Controller
    {
        private readonly IMembershipService _memberShipService;

        public MembershipController(IMembershipService memberShipService)
        {
            _memberShipService = memberShipService;
        }
        public IActionResult Index()
        {
            var MemberShips = _memberShipService.GetAllMemberShips();
            return View(MemberShips);
        }

        public IActionResult Create()
        {

            LoadDropDown();

            return View();

        }


        [HttpPost]
        public IActionResult Create(CreateMembershipViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var result = _memberShipService.CreateMemberShip(model);
                if (result)
                {
                    TempData["SuccessMessage"] = "Membership is Created Successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to Create Membership.";
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Membership cannot be created, Check your data!";
            LoadDropDown();
            return View(model);


        }
























        #region Helper Methods

        private void LoadDropDown()
        {
            var members = _memberShipService.GetMembersForDropDown();
            var plans = _memberShipService.GetPlansForDropDown();

            ViewBag.Members = new SelectList(members , "Id" , "Name");
            ViewBag.Plans = new SelectList(plans, "Id" , "Name");

        }


        #endregion







    }
}
