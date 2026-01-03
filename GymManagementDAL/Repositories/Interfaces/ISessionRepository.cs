using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        #region Inherited These Signatures from Genrice Repository
        //// GetAll.
        //IEnumerable<Session> GetAll();

        //// GetById.
        //Session? GetById(int Id);

        //// Add.
        //int Add(Session session);

        //// Update.
        //int Update(Session session);

        //// Delete.
        //int Delete(int Id); 
        #endregion


        IEnumerable<Session> GetAllSessionsWithTrainerAndCategory();

        int GetCountOfBookedSlots(int SessionId);
    }
}
