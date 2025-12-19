using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class Trainer : GymUser
    {
        // HireDate == CreatedAt of BaseEntity.
        public Specialties Specialties { get; set; }

        #region RelationShip Between Trainer and Session.

        // Navigational Property to Session Class.

        public ICollection<Session> TrainerSessions { get; set; } = null!;
        #endregion
    }
}
