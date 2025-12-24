using GymManagementBLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfcaes
{
    internal interface IMemberService
    {
        // GetAll.
        IEnumerable<MemberViewModel> GetAllMembers();

        // Create Model.
        bool CreateMember(CreateMemberViewModel createMember);

        // GetMemberDetails.
        MemberViewModel? GetMemberDetails(int MemberId);
        
        // Get Member Health Details.
        HealthRecordViewModel? GetMemberHealthDetails(int MemberId);

        // Get Member To Update.
        MemberToUpdateViewModel? GetMemberToUpdate(int MemberId);
    }
}
