using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IMemberRepository
    {
        // GetAll().
        IEnumerable<Member> GetAll();

        // GetById().
        Member GetById(int Id);

        // Add().
        int Add(Member Member);

        // Update().
        int Update(Member Member);

        // Delete().
        int Delete(int Id);

    }
}
