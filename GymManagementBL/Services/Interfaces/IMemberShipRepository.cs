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
    public interface IMemberShipRepository
    {
        IEnumerable<MemberShipViewModel> GetAllMemberShips();
        IEnumerable<MemberForSelectListViewModel> GetMembersForDropDown();
        IEnumerable<PlanForSelectListViewModel> GetPlansForDropDown();
        bool CreateMemberShip(CreateMemberShipViewModel Model);

    }
}
