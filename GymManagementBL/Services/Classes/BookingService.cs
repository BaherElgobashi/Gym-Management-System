using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.BookingViewModels;
using GymManagementBLL.ViewModels.MemberShipViewModel;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            
            _mapper = mapper;
        }

        public IEnumerable<MemberForSessionViewModel> GetAllMembersForSession(int id)
        {
            var BookingRepo = _unitOfWork.BookingRepository;

            var members = BookingRepo.GetSessionById(id);

            var MemberForSessionViewModels = _mapper.Map<IEnumerable<MemberForSessionViewModel>>(members);

            return MemberForSessionViewModels;
        }


        public IEnumerable<SessionViewModel> GetAllSessionsWithTrainerAndCategory()
        {
            var sessionRepo = _unitOfWork.SessionRepository;
            var sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            var sessionViewModels = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);

            foreach( var session in sessionViewModels)
            {
                session.AvaliableSlots = sessionRepo.GetCountOfBookedSlots(session.Id);
            }

            return sessionViewModels;

        }
        

        public IEnumerable<MemberForSelectListViewModel> GetMemberForDropdown(int id)
        {
            var BookingRepo = _unitOfWork.BookingRepository;

            var bookingMemberIds = BookingRepo.GetAll(s => s.Id == id)
                                              .Select(ms => ms.MemberId)
                                              .ToList();

            var MemberAvaliableToBook = _unitOfWork.GetRepository<Member>().GetAll(m => !bookingMemberIds.Contains(m.Id));

            var MemberSelectList = _mapper.Map<IEnumerable<MemberForSelectListViewModel>>(MemberAvaliableToBook);

            return MemberSelectList;
        }
        public bool CancelBooking(MemberAttendOrCancelViewModel model)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(model.SessionId);
                if (session is null || session.StartDate <= DateTime.Now) return false;

                
                var Booking = _unitOfWork.BookingRepository.GetAll(X => X.MemberId == model.MemberId && X.SessionId == model.SessionId)
                                                           .FirstOrDefault();
                if (Booking is null) return false;
                _unitOfWork.BookingRepository.Delete(Booking);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateBooking(CreateBookingViewModel model)
        {
            var session = _unitOfWork.SessionRepository.GetById(model.SessionId);

            if (session is null || session.StartDate <= DateTime.UtcNow)
                return false;

            var membershipRepo = _unitOfWork.MembershipRepository;
            var activeMembershipForMember = membershipRepo.GetFirstOrDefault(m => m.Status == "Active" && m.MemberId == model.MemberId);

            if (activeMembershipForMember is null)
                return false;

            var sessionRepo = _unitOfWork.SessionRepository;
            var bookedSlots = sessionRepo.GetCountOfBookedSlots(model.SessionId);

            var availableSlots = session.Capacity - bookedSlots;
            if (availableSlots <= 0)
                return false;

            var booking = _mapper.Map<MemberSession>(model);
            booking.IsAttended = false;

            _unitOfWork.BookingRepository.Add(booking);
            return _unitOfWork.SaveChanges() > 0;
        }

        

        public bool MemberAttended(MemberAttendOrCancelViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
