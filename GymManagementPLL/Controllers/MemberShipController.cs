using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
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
