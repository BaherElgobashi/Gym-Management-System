using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configuration
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("SessionCapacityCheck", "Capactity Between 1 and 25");

                Tb.HasCheckConstraint("SessionEndDateCheck", "EndDate > StartDate");
               
            });


            builder.HasOne(x => x.SessionCategory)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
