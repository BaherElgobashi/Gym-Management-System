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
    internal class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext _dbContext;
        public MemberRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public IEnumerable<Member> GetAll()
        {
            return _dbContext.Members.ToList();
        }
        public Member GetById(int Id)
        {
            var member = _dbContext.Members.Find(Id);
            if (member is null)
                return null;
            return member;
        }
        public int Add(Member Member)
        {
            _dbContext.Members.Add(Member);
             return _dbContext.SaveChanges();
        }

        public int Update(Member Member)
        {
            _dbContext.Members.Update(Member);
            return _dbContext.SaveChanges();
        }
        public int Delete(int Id)
        {
            var Member = _dbContext.Members.Find(Id);
            if(Member is null)
            {
                return 0;
            }
            
            _dbContext.Members.Remove(Member);
            return _dbContext.SaveChanges();
        }
    }
}
