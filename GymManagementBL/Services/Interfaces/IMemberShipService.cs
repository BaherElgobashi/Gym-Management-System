using GymManagementBLL.ViewModels.MemberShipViewModel;
using GymManagementBLL.ViewModels.MemberShipViewModels;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IMembershipService
    {
        IEnumerable<MembershipViewModel> GetAllMemberships();
        IEnumerable<MemberForSelectListViewModel> GetMembersForDropDown();
        IEnumerable<PlanForSelectListViewModel> GetPlansForDropDown();
        bool CreateMembership(CreateMembershipViewModel Model);
        bool DeleteMembership(int MemberId);

    }
}
