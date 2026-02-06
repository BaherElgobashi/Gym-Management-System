using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.PlanViewModels
{
    public class UpdatePlanViewModel
    {
        [Required(ErrorMessage ="Plan Name Is Required.")]
        [StringLength(50 , ErrorMessage ="Plan Name must be less than 51 Charactres.")]
        public string PlanName { get; set; } = null!;
         
        ////////////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage ="Description is Required.")]
        [StringLength(200, MinimumLength = 5 , ErrorMessage = "Description must be between 5 and 200 Characters.")]
        public string Description { get; set; } = null!;
        ////////////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage ="Duration Days Is Required.")]
        [Range(1,365, ErrorMessage = "Duration Days must be between 1 and 365 Day.")]
        public int DurationDays { get; set; }
        ////////////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage ="Price Is Required.")]
        [Range(0.1 , 1000 , ErrorMessage = "Price must be between 0.1 and 1000 in Price.")]
        public decimal Price { get; set; }
    }
}
