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
    internal class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext _dbContext;

        public TrainerRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Trainer> GetAll()
        {
            var Trainers = _dbContext.Trainers.ToList();
            return Trainers;
        }

        public Trainer? GetById(int Id)
        {
            var Trainer = _dbContext.Trainers.Find(Id);
            return Trainer;
        }
        public int Add(Trainer Trainer)
        {
            _dbContext.Trainers.Add(Trainer);
            return _dbContext.SaveChanges();
        }

        public int Update(Trainer Trainer)
        {
            _dbContext.Trainers.Update(Trainer);
            return _dbContext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var Trainer = _dbContext.Trainers.Find(Id);
            if (Trainer is null)
                return 0;

            _dbContext.Trainers.Remove(Trainer);
            
            return _dbContext.SaveChanges() ;
        }
    }
}
