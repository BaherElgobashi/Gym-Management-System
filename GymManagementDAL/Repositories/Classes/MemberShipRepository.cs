using GymManagementDAL.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class MemberShipRepository : GenericRepository<Membership>, IMemberShipRepository
    {
        private readonly GymDbContext _dbContext;
        public MemberShipRepository(GymDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Membership> GetAllMembershipsWithMembersAndPlans(Func<Membership, bool>? filter = null)
        {
            var MemberShips = _dbContext.Memberships.Include(m => m.Member)
                                                    .Include(p => p.Plan)
                                                    .Where(filter ?? (_ => true));
            return MemberShips;


        }

        public Membership? GetFirstOrDefault(Func<Membership, bool>? filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
