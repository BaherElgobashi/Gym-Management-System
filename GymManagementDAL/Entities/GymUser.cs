using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class GymUser : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
    class Address
    {
        public int BuildingNumber { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
