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
    public class BookingRepository : GenericRepository<MemberSession>, IBookingRepository
    {
        private readonly GymDbContext _dbContext;

        public BookingRepository( GymDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        MemberSession? IGenericRepository<MemberSession>.GetById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
