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

        public SessionViewModel? GetSessionById(int SessionId)
        {
            var Session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(SessionId);
            if(Session == null) return null;
            return new SessionViewModel()
            {
                Id = Session.Id,
                Description = Session.Description,
                StartDate = Session.StartDate,
                EndDate = Session.EndDate,
                Capacity = Session.Capacity,
                CategoryName = Session.SessionCategory.CategoryName,
                TrainerName = Session.SessionTrainer.Name,
                AvaliableSlots = Session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(SessionId)
            };
        }
    }
}
