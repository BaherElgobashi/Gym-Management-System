using GymManagementBLL.Services.Interfcaes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        #region Get All Plans.

        public IActionResult Index()
        {
            var Sessions = _sessionService.GetAllSessions();

            return View(Sessions);
        }

        #endregion

        #region Get Session Details.

        public IActionResult Details(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid ID.";
                return RedirectToAction(nameof(Index));
            }

            var Session = _sessionService.GetSessionById(id);
            if (Session == null) 
            {
                TempData["ErrorMessage"] = "Session is Not Found.";
                return RedirectToAction(nameof(Index));
            }
            return View(Session);

        }


        #endregion


        #region Create Session.

        public IActionResult Create()
        {
            LoadDropDownTrainers();
            LoadDropDownCategories();
            return View();
        }


        #endregion





        #region Delete Session.

        public IActionResult Delete(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid ID.";
                return RedirectToAction(nameof(Index));
            }

            var Session = _sessionService.GetSessionById(id);
            if (Session is null)
            {
                TempData["ErrorMessage"] = "Session is not Found.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = Session.Id;
            return View();


        }


        [HttpPost]

        public IActionResult DeleteConfirmed(int id)
        {
            bool Result = _sessionService.RemoveSession(id);
            if (Result)
            {
                TempData["ErrorMessage"] = "Session is Successfully.";
                
            }
            else
            {
                TempData["SuccessMessage"] = "Session can't be Deleted.";
            }
            return RedirectToAction(nameof(Index));
        }


        #endregion




        #region Helper Methods.


        private void LoadDropDownTrainers()
        {

            var Trainers = _sessionService.GetTrainerForDropDown();

            ViewBag.Trainers = new SelectList(Trainers , "Id" , "Name");

        }

        private void LoadDropDownCategories()
        {

            var Categories = _sessionService.GetCategoryForDropDown();

            ViewBag.Categories = new SelectList(Categories , "Id" ,"Name");

        }



        #endregion


    }
}
