using GymManagementBLL.Services.Interfcaes;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    [Authorize]
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

        [HttpPost]

        public IActionResult Create(CreateSessionViewModel CreatedSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDownTrainers();
                LoadDropDownCategories();
                return View(CreatedSession);
            }

            bool Result = _sessionService.CreateSession(CreatedSession);

            if (Result)
            {
                TempData["SuccessMessage"] = "Session is Created Succeessfully.";
                LoadDropDownTrainers();
                LoadDropDownCategories();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Session is Failed to be Created.";
                LoadDropDownTrainers();
                LoadDropDownCategories();
                return RedirectToAction(nameof(Index));
            }

        }







        #endregion




        #region Edit Session.

        public IActionResult Edit(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Id";
                return RedirectToAction(nameof(Index));
            }

            var Exists = _sessionService.GetSessionById(id);
            if(Exists is null)
            {
                TempData["ErrorMessage"] = "Session is Not Found." ;
                return RedirectToAction(nameof(Index));
            }

            var SessionToUpdate = _sessionService.GetSessionToUpdate(id);
            if(SessionToUpdate is null)
            {
                TempData["ErrorMessage"] = "You can't edit an ongoing or completed session.";
                return RedirectToAction(nameof(Index));
            }

            LoadDropDownTrainers();

            return View(SessionToUpdate);


        }


        [HttpPost]
        public IActionResult Edit([FromRoute] int id , UpdateSessionViewModel updatedSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDownTrainers();
                return RedirectToAction(nameof(Index));
            }

            bool Result = _sessionService.UpdateSession(updatedSession , id);
            if (Result) 
            {
                TempData["SuccessMessage"] = "Session is Updated Successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Edit Session!";
                LoadDropDownTrainers();
                return View(updatedSession);
            }

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
                TempData["SuccessMessage"] = "Session is Successfully.";
                
            }
            else
            {
                TempData["ErrorMessage"] = "Session can't be Deleted.";
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
