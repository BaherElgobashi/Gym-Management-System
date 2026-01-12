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
        public IActionResult Index()
        {
            var Members = _memberService.GetAllMembers();
            return View(Members);
        }
    }
}
