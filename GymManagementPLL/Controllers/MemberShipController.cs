using GymManagementBLL.Services.Classes;
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
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService memberShipService)
        {
            _membershipService = memberShipService;
        }
        public IActionResult Index()
        {
            var MemberShips = _membershipService.GetAllMemberships();
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
                var result = _membershipService.CreateMembership(model);
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

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var result = _membershipService.DeleteMembership(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Membership cancelled successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete membership!";
            }
            return RedirectToAction(nameof(Index));
        }





















        #region Helper Methods

        private void LoadDropDown()
        {
            var members = _membershipService.GetMembersForDropDown();
            var plans = _membershipService.GetPlansForDropDown();

            ViewBag.Members = new SelectList(members , "Id" , "Name");
            ViewBag.Plans = new SelectList(plans, "Id" , "Name");

        }


        #endregion







    }
}
