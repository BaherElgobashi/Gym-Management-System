using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.BookingViewModels;
using GymManagementBLL.ViewModels.MemberShipViewModel;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public IEnumerable<SessionViewModel> GetAllSessionsWithTrainerAndCategory()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<MemberForSessionViewModel> GetAllMembersForSession(int id)
        {
            throw new NotImplementedException();
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
