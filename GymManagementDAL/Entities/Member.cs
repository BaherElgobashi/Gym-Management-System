using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class Member : GymUser
    {
        // JoinDate == CreatedAt.

        public string? Photo { get; set; }
        #region Member - HealthRecord Relationship.

        public HealthRecord HealthRecord { get; set; } = null!;
        #endregion

        #region Member - Membership Relationship.

        // Navigational property to Membership Class.
        public ICollection<Membership> Memberships { get; set; } = null!;
        #endregion

        public ICollection<MemberSession> MemberSessions { get; set; } = null!;

    }
}
