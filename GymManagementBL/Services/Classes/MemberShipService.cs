using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberShipViewModel;
using GymManagementBLL.ViewModels.MemberShipViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections;
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
            var MembersForDropDown = _unitOfWork.GetRepository<Member>().GetAll();

            var MembersForSelectViewModel = _mapper.Map<IEnumerable<MemberForSelectListViewModel>>(MembersForDropDown);

            return MembersForSelectViewModel;
        }

        public IEnumerable<PlanForSelectListViewModel> GetPlansForDropDown()
        {
            var PlansForDropDown = _unitOfWork.GetRepository<Plan>().GetAll();

            var PlansForSelectViewModel = _mapper.Map<IEnumerable<PlanForSelectListViewModel>>(PlansForDropDown);

            return PlansForSelectViewModel;
        }
        public bool CreateMemberShip(CreateMemberShipViewModel Model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMemberShip(int MemberId)
        {
            throw new NotImplementedException();
        }

        #region Helper Methods.

        private bool IsMemberExists(int memberId)
        {
            return _unitOfWork.GetRepository<Member>().GetById(memberId) is not null;
        }

        private bool IsPlanExists(int planId)
        {
            return _unitOfWork.GetRepository<Plan>().GetById(planId) is not null;

        }

        private bool HasActiveMemberShip(int memberId)
        {
            return _unitOfWork.MembershipRepository
                    .GetAllMembershipsWithMembersAndPlans(m => m.MemberId == memberId && m.Status == "Active").Any();
        }

        #endregion


    }
}
