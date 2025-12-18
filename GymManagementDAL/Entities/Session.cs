using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class Session : BaseEntity
    {
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #region RelationShip Between Trainer and Category.
        // Foreign key to Category Class.
        public int CategoryId { get; set; }

        // Navigational Propery to Category Class.

        public Category SessionCategory { get; set; } = null!;
        #endregion


        #region RelationShip Between Trainer and Session.

        //Foreign Key to Trainer Class.
        public int TrainerId { get; set; }

        // Navigational property to Trainer Class.

        public Trainer SessionTrainer { get; set; } = null!;

        #endregion


        public ICollection<MemberSession> SessionMembers { get; set; } = null!;
    }
}
