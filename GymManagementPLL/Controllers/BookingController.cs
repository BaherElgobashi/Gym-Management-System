using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.BookingViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public IActionResult Index()
        {
            var sessions = _bookingService.GetAllSessionsWithTrainerAndCategory(); 
            return View(sessions);
        }

        public IActionResult GetMembersForUpcomingSession(int id)
        {
            var members = _bookingService.GetAllMembersForSession(id);
            return View(members);
        }
        public IActionResult GetMembersForOngoingSession(int id)
        {
            var members = _bookingService.GetAllMembersForSession(id);
            return View(members);
        }


        public IActionResult Create(int id)
        {
            var members = _bookingService.GetMemberForDropdown(id);
            var membersSelectList = new SelectList(members, "Id", "Name");
            ViewBag.Members = membersSelectList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateBookingViewModel model)
        {
            var result = _bookingService.CreateBooking(model);

            if (result)
            {
                TempData["SuccessMessage"] = "Booking created successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Booking failed. Member must have an active membership.";
            }

            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { id = model.SessionId });
        }


        [HttpPost]
        public IActionResult Attended(MemberAttendOrCancelViewModel model)
        {
            var result = _bookingService.MemberAttended(model);

            if (result)
            {
                TempData["SuccessMessage"] = "Member attended successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member attendance can't be marked";
            }
            return RedirectToAction(nameof(GetMembersForOngoingSession), new { id = model.SessionId });
        }

        [HttpPost]
        public IActionResult Cancel(MemberAttendOrCancelViewModel model)
        {
            var result = _bookingService.CancelBooking(model);

            if (result)
            {
                TempData["SuccessMessage"] = "Booking cancelled successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Booking can't be cancelled";
            }
            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { id = model.SessionId });
        }
    }
}
