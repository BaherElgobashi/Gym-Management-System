using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }


        #region Plan - Membership Relationship.
        // Navigational property to Membership Class.
        public ICollection<Membership> Membership { get; set; } = null!; 
        #endregion
    }
}
