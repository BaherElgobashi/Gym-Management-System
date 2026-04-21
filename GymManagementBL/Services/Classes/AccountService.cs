using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AccountViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(UserManager<ApplicationUser>userManager,
                               IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
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

        public async Task<IEnumerable<UserViewModel>> GetUserAsync()
        {
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var superAdminUsers = await _userManager.GetUsersInRoleAsync("SuperAdmin");
            if ((adminUsers == null || !adminUsers.Any()) && (superAdminUsers == null || !superAdminUsers.Any()))
                return [];
            bool isAdminUserExist = false;
            IEnumerable<UserViewModel> AdminModel = Enumerable.Empty<UserViewModel>();
            if (adminUsers is not null)
            {
                AdminModel = adminUsers.Select(x => new UserViewModel
                {
                    UserId = x.Id,
                    FullName = $"{x.FirstName.Trim()} {x.LastName.Trim()}",
                    Email = x.Email,
                    Role = "Admin"
                });

                if (superAdminUsers is null)
                    return AdminModel;
                isAdminUserExist = true;
            }
            var superAdminModel = superAdminUsers.Select(x => new UserViewModel
            {
                FullName = $"{x.FirstName.Trim()} {x.LastName.Trim()}",
                UserId = x.Id,
                Email = x.Email,
                Role = "SuperAdmin"
            });

            if (isAdminUserExist)
            {
                return AdminModel.Concat(superAdminModel);
            }
            return superAdminModel;
        }


        public async Task<bool> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return false;
            var rolesForUser = await _userManager.GetRolesAsync(user);
            try
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    if (rolesForUser is not null && rolesForUser.Any())
                    {
                        await _userManager.RemoveFromRolesAsync(user, rolesForUser);
                    }
                    var result = await _userManager.DeleteAsync(user);
                    await transaction.CommitAsync();
                    return result.Succeeded;
                }
            }
            catch
            {
                return false;
            }

        }


    }
}
