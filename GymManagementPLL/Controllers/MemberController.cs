using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfcaes;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService )
        {
            _memberService = memberService;
        }

        #region Get All Members.

        // Get All Members
        public IActionResult Index()
        {
            var Members = _memberService.GetAllMembers();
            return View(Members);
        }
        #endregion

        #region Get Member Details.

        public IActionResult MemberDetails(int id)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            var Member = _memberService.GetMemberDetails(id);
            if (Member == null)
                return RedirectToAction(nameof(Index));

            return View(Member);

        }

        #endregion



        #region Health Record Details.

        public IActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            var HealthRecordDetails = _memberService.GetMemberHealthDetails(id);
            if(HealthRecordDetails is null)
                return RedirectToAction(nameof(Index));

            return View(HealthRecordDetails);
        }


        #endregion


    }
}
