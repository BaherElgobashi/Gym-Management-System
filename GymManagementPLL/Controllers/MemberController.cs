using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfcaes;
using GymManagementBLL.ViewModels.MemberViewModels;
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
            {
                TempData["ErrorMessage"] = "Id must be 0 or Negative Number.";
                return RedirectToAction(nameof(Index));
            }
               

            var Member = _memberService.GetMemberDetails(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member is not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(Member);

        }

        #endregion



        #region Health Record Details.

        public IActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id can't be 0 or Negative Number.";
                return RedirectToAction(nameof(Index));
            }

            var HealthRecordDetails = _memberService.GetMemberHealthDetails(id);
            if(HealthRecordDetails is null)
            {
                TempData["ErrorMessage"] = "Health Record is not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(HealthRecordDetails);
        }


        #endregion


        #region Create Member.
         
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMember(CreateMemberViewModel CreatedMember)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid.","Check Data and Missing Fields.");
                return View(nameof(Create), CreatedMember);
            }

            bool Result = _memberService.CreateMember(CreatedMember);

            if (Result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed To Create , Check Phone and Email.";
            }

            return RedirectToAction(nameof(Index));
        

        }

        #endregion

        #region  Edit Member

        public IActionResult MemberEdit(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Id can't be 0 or Negative Number.";
                return RedirectToAction(nameof(Index));
            }

            var Member = _memberService.GetMemberToUpdate(id);
            if(Member is null)
            {
                TempData["ErrorMessage"] = "Member is not Found.";

                return RedirectToAction(nameof(Index));
            }

            return View(Member);

        }

        [HttpPost]
        public IActionResult MemberEdit([FromRoute] int id , MemberToUpdateViewModel MemberToEdit)
        {
            if (!ModelState.IsValid)
                return View(MemberToEdit);

            var Result = _memberService.UpdateMemberDetails(id, MemberToEdit);

            if (Result)
            {
                TempData["SuccessMessage"] = "Member is Updated Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Member is Failed to Update.";
            }
            return RedirectToAction(nameof(Index));

        }



        #endregion


        #region Delete Members.

        public IActionResult Delete(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Id can't be 0 or Negative Number.";
                return RedirectToAction(nameof(Index));
            }

            var Member = _memberService.GetMemberDetails(id);

            if(Member is null)
            {
                TempData["ErrorMessage"] = "Member is not Found.";
                return RedirectToAction(nameof(Index));
            }

            return View();

        }

        #endregion


    }
}
