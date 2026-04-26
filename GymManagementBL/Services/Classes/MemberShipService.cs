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
    public class MembershipService : IMembershipService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MembershipService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public IEnumerable<MembershipViewModel> GetAllMemberShips()
        {
            var MemberShips = _unitOfWork.MembershipRepository.GetAllMembershipsWithMembersAndPlans( m => m.Status == "Active");

            var MemberShipsViewModels = _mapper.Map<IEnumerable<MembershipViewModel>>(MemberShips);

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
        public bool CreateMemberShip(CreateMembershipViewModel Model)
        {
            if(IsMemberExists(Model.MemberId) && IsPlanExists(Model.PlanId) && HasActiveMemberShip(Model.MemberId))
            {
                return false;
            }

            var membershipRepo = _unitOfWork.MembershipRepository;

            var memberShipcreate = _mapper.Map<Membership>(Model);

            var plan = _unitOfWork.GetRepository<Plan>().GetById(Model.PlanId);

            memberShipcreate.EndDate = DateTime.Now.AddDays(plan!.DurationDays);

            membershipRepo.Add(memberShipcreate);

            return _unitOfWork.SaveChanges() > 0;

        }

        public bool DeleteMemberShip(int MemberId)
        {
            var membershiprepo = _unitOfWork.MembershipRepository;

            var memberToDelete = membershiprepo.GetFirstOrDefault(m => m.MemberId == MemberId && m.Status == "Active");

            if(memberToDelete != null)
            {
                return false;
            }

            membershiprepo.Delete(memberToDelete);

            return _unitOfWork.SaveChanges() > 0;


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
