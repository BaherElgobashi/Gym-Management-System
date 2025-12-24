using GymManagementDAL.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _dbContext;
        public PlanRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Plan> GetAll()
        {
            var plans = _dbContext.Plans.ToList();
            return plans;
        }

        public Plan? GetById(int Id)
        {
            var plan = _dbContext.Plans.Find(Id);

            return plan;
        }

        public int Update(Plan plan)
        {
            _dbContext.Plans.Update(plan);

            return _dbContext.SaveChanges();
        }
    }
}
