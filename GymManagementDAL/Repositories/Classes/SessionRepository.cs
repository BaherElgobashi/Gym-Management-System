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
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbcontext;

        public SessionRepository(GymDbContext dbcontext):base(dbcontext)
        {
            _dbcontext = dbcontext;
        }




        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
            var SessionsWithTrainerAndCategory = _dbcontext.Sessions
                                                .Include(x => x.SessionTrainer)
                                                .Include(x => x.SessionCategory)
                                                .ToList(); 
           
            return SessionsWithTrainerAndCategory;
        }
        public int GetCountOfBookedSlots(int SessionId)
        {
            return _dbcontext.MemberSessions.Count(x=>x.SessionId == SessionId);
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return _dbcontext.Sessions.Include(x=>x.SessionTrainer)
                                      .Include(x=>x.SessionCategory)
                                      .FirstOrDefault(x=>x.Id == sessionId);
        }
    }
}
