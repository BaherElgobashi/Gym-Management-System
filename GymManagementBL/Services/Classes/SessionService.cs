using GymManagementBLL.Services.Interfcaes;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();

            if (Sessions == null) return [];

            return Sessions.Select(S => new SessionViewModel
            {
                Id = S.Id,

                Description = S.Description,

                StartDate = S.StartDate,

                EndDate = S.EndDate,

                Capacity = S.Capacity,
                TrainerName = S.SessionTrainer.Name,

                CategoryName = S.SessionCategory.CategoryName,

                AvaliableSlots = S.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(S.Id),

            });

        }
    }
}
