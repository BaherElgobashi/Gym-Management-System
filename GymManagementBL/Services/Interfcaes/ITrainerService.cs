using GymManagementBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfcaes
{
    internal interface ITrainerService
    {
        // Get All Trainers.
        IEnumerable<TrainerViewModel> GetAllTrainers();

        // Get Trainer Details.
        TrainerViewModel GetTrainerDetails(int TrainerId);

        // Create Trainer.
        bool CreateTrainer(CreateTrainerViewModel CreateTrainer);
    }
}
