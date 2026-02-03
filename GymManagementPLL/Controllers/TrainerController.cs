using GymManagementBLL.Services.Interfcaes;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;
        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        
        public IActionResult Index()
        {
            var Trainers = _trainerService.GetAllTrainers();
            return View(Trainers);
        }

        #region Get Trainer Details
        public IActionResult TrainerDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id can't be Zero or Negative Number";
                return RedirectToAction(nameof(Index));
            }


            var Trainer = _trainerService.GetTrainerDetails(id);

            if (Trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer is not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Trainer);

        }
        #endregion


        #region Create Trainer

        public IActionResult Create()
        {
            return View();
        }



        #endregion
    }
}
