using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberShipViewModel;
using GymManagementBLL.ViewModels.MemberShipViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class MemberShipService : IMemberShipService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MemberShipService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public IEnumerable<MemberShipViewModel> GetAllMemberShips()
        {
            var MemberShips = _unitOfWork.MembershipRepository.GetAllMembershipsWithMembersAndPlans( m => m.Status == "Active");

            var MemberShipsViewModels = _mapper.Map<IEnumerable<MemberShipViewModel>>(MemberShips);

            return MemberShipsViewModels;

        }

        public IEnumerable<MemberForSelectListViewModel> GetMembersForDropDown()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlanForSelectListViewModel> GetPlansForDropDown()
        {
            throw new NotImplementedException();
        }
        public bool CreateMemberShip(CreateMemberShipViewModel Model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMemberShip(int MemberId)
        {
            throw new NotImplementedException();
        }

        
    }
}
