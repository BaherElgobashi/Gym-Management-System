using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.TrainerViewModels
{
    internal class MemberToUpdateViewModel
    {
        public string? Name { get; set; }

        /////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)] 
        [EmailAddress(ErrorMessage = "Email is Invalid.")] // UI Hint.
        [StringLength(100 , MinimumLength = 5 , ErrorMessage = "Email must be between 5 and 100 Characters")]
        public string Email { get; set; } = null!;


        /////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage = "Phone is Required.")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Phonenumber is Invalid.")]
        [RegularExpression(@"^010|011|012|015\d{8}$" , ErrorMessage = "Phonenumber must be an Egyptian Phonenumber.")]
        public string Phone { get; set; } = null!;

        /////////////////////////////////////////////////////////////////////////



        [Required(ErrorMessage = "Building Number is Required.")]
        [Range(1 , 9000 , ErrorMessage = "Building Number must be between 1 and 9000")]
        public int BuildingNumber { get; set; }

        /////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage = "Street is Required.")]
        [StringLength(30 , MinimumLength = 2 , ErrorMessage = "Street must be between 2 and 30 Characters.")]
        public string Street { get; set; } = null!;


        /////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage = "City is Required.")]
        [StringLength(30 , MinimumLength = 2 , ErrorMessage = "City must be between 2 and 30 Characters.")]
        [RegularExpression(@"^[a-zA-Z]\s+$" , ErrorMessage = "City must contain Letters and Spaces only.")]
        public string City { get; set; }


        /////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage = "Choose one of Specialities is Required.")]
        public Specialties Specialties { get; set; }







    }
}
