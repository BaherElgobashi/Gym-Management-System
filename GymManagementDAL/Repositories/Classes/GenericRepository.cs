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
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity , new()
    {
        private readonly GymDbContext _dbContext;
        public GenericRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public IEnumerable<TEntity> GetAll()
        {
            var entities = _dbContext.Set<TEntity>().AsNoTracking().ToList();

            return entities;
        }

        public TEntity? GetById(int Id)
        {
            var entity = _dbContext.Set<TEntity>().Find(Id);

            return entity;
        }
        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);

            return _dbContext.SaveChanges();
        }
        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);

            return _dbContext.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);

            return _dbContext.SaveChanges();
        }

        

        
    }
}
