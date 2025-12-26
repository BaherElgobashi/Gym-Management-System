using GymManagementBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfcaes
{
    internal interface IPlanService
    {
        // Get All Plans.
        IEnumerable<PlanViewModel>GetAllPlans();

        // Get Details of one plan only by using Id.
        PlanViewModel? GetPlanById(int PlanId);

        // Get the plan to make updates on it.
        UpdatePlanViewModel? GetPlanToUpdate(int PlanId);

        // Make Updates to the plan.
        bool UpdatePlan(int PlanId , UpdatePlanViewModel UpdatedPlan);

        // Make Activation or DeActivation.
        bool ToggleStatus(int PlanId);
    }
}
