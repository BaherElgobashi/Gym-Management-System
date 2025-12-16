using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class Trainer : GymUser
    {
        // HireDate == CreatedAt of BaseEntity.
        public Specialties Specialties { get; set; }
    }
}
