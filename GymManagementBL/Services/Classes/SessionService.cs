using AutoMapper;
using GymManagementBLL.Services.Interfcaes;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
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
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();

            if (Sessions == null) return [];

            #region Manual Mapping.
            //return Sessions.Select(S => new SessionViewModel
            //{
            //    Id = S.Id,

            //    Description = S.Description,

            //    StartDate = S.StartDate,

            //    EndDate = S.EndDate,

            //    Capacity = S.Capacity,

            //    TrainerName = S.SessionTrainer.Name,

            //    CategoryName = S.SessionCategory.CategoryName,

            //    AvaliableSlots = S.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(S.Id),

            //}); 
            #endregion

            #region Using AutoMapper in Mapping.


            // Using AutoMapper in Mapping

            var MappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(Sessions);

            foreach (var Session in MappedSessions)
            {
                Session.AvaliableSlots = Session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(Session.Id);

            } 
            #endregion

            return MappedSessions;


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
