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
    internal class SessionRepository : ISessionRepository
    {
        private readonly GymDbContext _dbContext;
        public SessionRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Session> GetAll()
        {
            var sessions = _dbContext.Sessions.ToList();
            return sessions;
        }

        public Session GetById(int Id)
        {
            var session = _dbContext.Sessions.Find(Id);
            return session;
        }
        public int Add(Session session)
        {
            _dbContext.Sessions.Add(session);
            return _dbContext.SaveChanges();
        }

        public int Update(Session session)
        {
            _dbContext.Sessions.Update(session);
            return _dbContext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var session = _dbContext.Sessions.Find(Id);

            if (session is null) return 0;
            _dbContext.Sessions.Remove(session);

            return _dbContext.SaveChanges();
        }
    }
}
