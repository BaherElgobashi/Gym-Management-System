using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AccountViewModels;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser>userManager)
        {
            _userManager = userManager;
        }


        public async Task<ApplicationUser?> ValidateUserAsync(LoginViewModel loginViewModel)
        {
            var User = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (User is null)
                return null;

            var IsPassword = await _userManager.CheckPasswordAsync(User , loginViewModel.Password);

            return IsPassword ? User : null;    
            


        }


        public async Task<IdentityResult> RegisterAsync(CreateNewUser Model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = Model.FirstName,
                LastName = Model.LastName,
                Email = Model.Email,
                UserName = Model.Username

            };

            var Result = await _userManager.CreateAsync(user,Model.Password);
            if (Result.Succeeded)
            {
                var RoleResult = await _userManager.AddToRoleAsync(user, Model.Role.ToString());
                if (RoleResult.Succeeded) 
                {
                    var Errors = RoleResult.Errors.ToArray();
                    return IdentityResult.Failed(Errors);
                }
            }
            return Result;
        }







    }
}
