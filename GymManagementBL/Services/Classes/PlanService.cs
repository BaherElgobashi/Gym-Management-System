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
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);

            if(Plan == null || Plan.IsActive == false || HasActiveMemberShips(PlanId)) return null;

            var GetMemberToViewModel = new UpdatePlanViewModel() 
            {
                PlanName = Plan.Name,

                Description = Plan.Description,

                DurationDays = Plan.DurationDays,

                Price = Plan.Price,

            };
            return GetMemberToViewModel;
        }

       

        public bool UpdatePlan(int PlanId, UpdatePlanViewModel UpdatedPlan)
        {
            try
            {
                var Plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);

                if (Plan is null || HasActiveMemberShips(PlanId)) return false;

                // First way to update.

                //Plan.Description = UpdatedPlan.Description;
                //Plan.DurationDays = UpdatedPlan.DurationDays;
                //Plan.Price = UpdatedPlan.Price;
                //Plan.UpdatedAt = DateTime.Now;

                // Second Way to update.
                (Plan.Description, Plan.DurationDays, Plan.Price, Plan.UpdatedAt)
                    = (UpdatedPlan.Description, UpdatedPlan.DurationDays, UpdatedPlan.Price, DateTime.Now);

                _unitOfWork.GetRepository<Plan>().Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool ToggleStatus(int PlanId)
        {
            var Repo = _unitOfWork.GetRepository<Plan>();

            var Plan = Repo.GetById(PlanId);

            if (Plan is null || HasActiveMemberShips(PlanId)) return false;

            Plan.IsActive = Plan.IsActive == true ? false : true;

            Plan.UpdatedAt = DateTime.Now;

            try
            {
                Repo.Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }


        #region Helper Methods

        private bool HasActiveMemberShips(int PlanId) 
        {
            var ActiveMemberShips = _unitOfWork.GetRepository<Membership>()
                .GetAll(x => x.Id == PlanId && x.Status == "Active");

            return ActiveMemberShips.Any();

        }
        #endregion
    }
}
