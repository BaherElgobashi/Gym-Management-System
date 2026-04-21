using GymManagementBLL.ViewModels.AccountViewModels;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUser?> ValidateUserAsync(LoginViewModel loginViewModel);

        Task<IdentityResult> RegisterAsync(CreateNewUser Model);

        Task<IEnumerable<UserViewModel>> GetUserAsync();

        Task<bool> Delete(string userId);
       
    }
}
