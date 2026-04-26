using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.BookingViewModels;
using GymManagementBLL.ViewModels.MemberShipViewModel;
using GymManagementBLL.ViewModels.SessionViewModels;
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
            throw new NotImplementedException();
        }
        public bool CancelBooking(MemberAttendOrCancelViewModel model)
        {
            throw new NotImplementedException();
        }

        public bool CreateBooking(CreateBookingViewModel model)
        {
            throw new NotImplementedException();
        }

        

        public bool MemberAttended(MemberAttendOrCancelViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
