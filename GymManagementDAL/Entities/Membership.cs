using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class Membership : BaseEntity
    {
        // Start Properties.

        // StartDate is the Same of CreatedAt from BaseEntity.

        public DateTime EndDate { get; set; }

        // Readonly Propery

        public string Status 
        {
            get
            {
                if (EndDate >= DateTime.Now)
                {
                    return "Expired.";

                }
                else
                {
                    return "Active.";
                }
            }
        
        }



        // End Properties.






        //////////////////////////////////////////////////////

        // Start Relationships.

        // Primary Key to Member Class.
        public int MemberId { get; set; }

        // Navigational Property to Member Class.
        public Member Member { get; set; } = null!;


        // Primary Key to Plan Class.
        public int PlanId { get; set; }

        // Navigational Property to Plan Class.

        public Plan Plan { get; set; } = null!;

        // End Relationships.
    }
}
