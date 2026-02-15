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
    public class SessionService : ISessionService
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
            try
            {
                // Check if Trainer Exists.

                if (!IsTrainerExists(CreatedSession.TrainerId)) return false;

                // Check if Category Exists.

                if (!IsCategoryExists(CreatedSession.CategoryId)) return false;

                // Check if StartDate is before EndDate. 

                if (!IsDateTimeValid(CreatedSession.StartDate, CreatedSession.EndDate)) return false;

                if (CreatedSession.Capacity > 25 || CreatedSession.Capacity <= 0) return false;

                var SessionEntity = _mapper.Map<Session>(CreatedSession);

                _unitOfWork.GetRepository<Session>().Add(SessionEntity); // Add Locally.

                return _unitOfWork.SaveChanges() > 0; // Added To DataBase.
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Create Session Failed : {ex}");
                return false;
            }

        }


        public UpdateSessionViewModel? GetSessionToUpdate(int SessionId)
        {
            var Session = _unitOfWork.GetRepository<Session>().GetById(SessionId);

            if(!IsSessionAvaliableForUpdating(Session!)) return null;

            return _mapper.Map<Session , UpdateSessionViewModel>(Session!);




            

        }

        public bool UpdateSession(UpdateSessionViewModel UpdatedSession, int SessionId)
        {
            try
            {

                var Session = _unitOfWork.SessionRepository.GetById(SessionId);

                if(Session == null) return false;

                if(!IsSessionAvaliableForUpdating(Session!)) return false;

                if(!IsTrainerExists(UpdatedSession.TrainerId)) return false;

                if(!IsDateTimeValid(UpdatedSession.StartDate , UpdatedSession.EndDate)) return false;

                _mapper.Map(UpdatedSession, Session);

                Session.UpdatedAt = DateTime.Now;

                _unitOfWork.SessionRepository.Update(Session); // Updated Locally.

                return _unitOfWork.SaveChanges() > 0; // Updated To Databse.



            }
            catch(Exception ex)
            {
                Console.WriteLine($"Update Session Failed : {ex}");

                return false;
            }
        }


        public bool RemoveSession(int SessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetById(SessionId);
                if(Session == null) return false;

                if(!IsSessionAvaliableForRemoving(Session)) return false;

                _unitOfWork.SessionRepository.Delete(Session); // Deleted Locally.

                return _unitOfWork.SaveChanges() > 0; // Deleted From Database.

            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Delete Session Failed : {ex}");

                return false;
            }
        }





        public IEnumerable<TrainerSelectViewModel> GetTrainerForDropDown()
        {
            
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll() ?? [];


            return _mapper.Map<IEnumerable< TrainerSelectViewModel>>(Trainers);

        }

        public IEnumerable<CategorySelectViewModel> GetCategoryForDropDown()
        {

            var Categories = _unitOfWork.GetRepository<Category>().GetAll() ?? [];

            return _mapper.Map<IEnumerable< CategorySelectViewModel>>(Categories);
                

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

        private bool IsSessionAvaliableForUpdating(Session session)
        {
            // If Session Completed - No Updates Avaliable.
            if(session.EndDate < DateTime.Now) return false;

            // If Session Started - No Updates Avaliable.
            if (session.StartDate <= DateTime.Now) return false;


            // If Session HasActiveBooking - No Updates Avaliable.
            var HasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if(HasActiveBooking) return false;


            return true;

        }



        private bool IsSessionAvaliableForRemoving(Session session)
        {

            // If Session Started - No Delete Avaliable.
            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;

            //// If Session Upcomming - No Delete Avaliable.
            //if (session.StartDate > DateTime.Now) return false;


            // If Session HasActiveBooking - No Delete Avaliable.
            var HasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (HasActiveBooking) return false;


            return true;

        }

        





        #endregion
    }
}
