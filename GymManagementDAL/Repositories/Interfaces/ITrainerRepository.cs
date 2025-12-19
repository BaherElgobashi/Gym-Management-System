using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface ITrainerRepository
    {
        // GetAll.
        IEnumerable<Trainer> GetAll();

        // GetById.
        Trainer? GetById(int Id);

        // Add.
        int Add(Trainer Trainer);

        // Update.
        int Update(Trainer Trainer);

        // Delete.
        int Delete(int Id);
    }
}
