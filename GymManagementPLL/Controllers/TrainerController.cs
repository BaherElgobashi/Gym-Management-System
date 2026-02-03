using GymManagementBLL.Services.Interfcaes;
using GymManagementBLL.ViewModels.TrainerViewModels;
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

        [HttpPost]
        public IActionResult CreateTrainer(CreateTrainerViewModel CreateTrainer)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check data and Missing Fields.");
                return View(nameof(Create), CreateTrainer);
            }

            bool Result = _trainerService.CreateTrainer(CreateTrainer);
            if (Result)
            {
                TempData["SuccesMessage"] = "Trainer is Created Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed to be Created , Check your Own data.";
            }
            return RedirectToAction(nameof(Index));
        }



        #endregion



        #region Edit Trainer

        public IActionResult EditTrainer(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Id can't be Zero or Negative Number.";
                return RedirectToAction(nameof(Index));
            }

            var Trainer = _trainerService.GetTrainerToUpdate(id);

            if(Trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer is not Found.";
                return RedirectToAction(nameof(Index));
            }
            return View(Trainer);

        }


        [HttpPost]
        public IActionResult EditTrainer([FromRoute] int id , TrainerToUpdateViewModel TrainerToEdit)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check data and Missing Fields.");
                return View(TrainerToEdit);
            }

            bool Result = _trainerService.UpdateTrainerDetails(id, TrainerToEdit);
            if (Result)
            {
                TempData["SuccessMessage"] = "Trainer is Updated Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer is Failed to be Updated.";
            }
            return RedirectToAction(nameof(Index));
        }


        #endregion


        #region Remove Trainer.

        public IActionResult DeleteTrainer(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Id can't be Zero or Negative Number.";
            }

            var Trainer = _trainerService.GetTrainerDetails(id);
            if(Trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer is not Found.";
                return RedirectToAction(nameof(Index));

            }
            ViewBag.TrainerId = id;
            ViewBag.TrainerName = Trainer.Name;
            return View();
        }


        #endregion


    }
}
