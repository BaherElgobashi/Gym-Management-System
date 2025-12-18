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

    }
}
