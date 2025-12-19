using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity , new()
    {
        // GetAll.
        IEnumerable<TEntity> GetAll();

        // GetById.
        TEntity? GetById(int Id);

        // Add.
        int Add(TEntity entity);

        // Update.
        int Update(TEntity entity); 

        // Delete.
        int Delete(TEntity entity); 
    }
}
