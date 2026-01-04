using AutoMapper;
using GymManagementBLL.Services.Interfcaes;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
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

            #region Manual Mapping.

            //return new SessionViewModel()
            //{
            //    Id = Session.Id,
            //    Description = Session.Description,
            //    StartDate = Session.StartDate,
            //    EndDate = Session.EndDate,
            //    Capacity = Session.Capacity,
            //    CategoryName = Session.SessionCategory.CategoryName,
            //    TrainerName = Session.SessionTrainer.Name,
            //    AvaliableSlots = Session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(SessionId)
            //}; 
            #endregion
             
            #region Using AutoMapper.

            var MappedSession = _mapper.Map<Session, SessionViewModel>(Session);

            MappedSession.AvaliableSlots = MappedSession.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(MappedSession.Id); 
            #endregion

            return MappedSession;
        }



        public bool CreateSession(CreateSessionViewModel CreatedSession)
        {
            // Check if Trainer Exists.

            if(!IsTrainerExists(CreatedSession.TrainerId)) return false;

            // Check if Category Exists.

            if (!IsCategoryExists(CreatedSession.CategoryId)) return false;

            // Check if StartDate is before EndDate. 

            if(!IsDateTimeValid(CreatedSession.StartDate , CreatedSession.EndDate)) return false;


        }




        #region Helper Methods.
        private bool IsTrainerExists(int TrainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(TrainerId) is not null;
        }

        private bool IsCategoryExists(int CategoryId)
        {
            return _unitOfWork.GetRepository<Category>().GetById(CategoryId) is not null;
        }

        private bool IsDateTimeValid(DateTime StartDate , DateTime EndDate)
        {
            return StartDate < EndDate;
        }

        #endregion
    }
}
