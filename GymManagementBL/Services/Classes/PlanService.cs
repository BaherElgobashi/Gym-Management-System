using GymManagementBLL.Services.Interfcaes;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            throw new NotImplementedException();
        }

        public PlanViewModel? GetPlanById(int PlanId)
        {
            throw new NotImplementedException();
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int PlanId)
        {
            throw new NotImplementedException();
        }

       

        public bool UpdatePlan(int PlanId, UpdatePlanViewModel UpdatedPlan)
        {
            throw new NotImplementedException();
        }

        public bool ToggleStatus(int PlanId)
        {
            throw new NotImplementedException();
        }
    }
}
