using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    // Make One-To-One Relationship with Member Class.
    // has shared Primary Key inherited from BaseEntity Class.
    public class HealthRecord : BaseEntity
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string Blood { get; set; } = null!;
        public string? Notes { get; set; } = null!;
        

    }
}
