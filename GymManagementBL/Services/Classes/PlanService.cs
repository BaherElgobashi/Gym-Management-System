using GymManagementBLL.Services.Interfcaes;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Entities;
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
            var Plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (Plans is null || !Plans.Any())
                return [];

            var PlanViewModels = Plans.Select(P=> new PlanViewModel 
            {
                Id = P.Id,
                Name = P.Name,
                Description = P.Description,
                DurationDays = P.DurationDays,
                Price = P.Price,
                IsActive = P.IsActive,

            });
            return PlanViewModels;
        }

        public PlanViewModel? GetPlanById(int PlanId)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if(Plan == null) return null;

            var PlanViewModel = new PlanViewModel() 
            {
                Id = Plan.Id,
                Name = Plan.Name,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                Price = Plan.Price,
                IsActive = Plan.IsActive,
            
            };
            return PlanViewModel;
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
