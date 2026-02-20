using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AccountViewModels;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountService accountService , SignInManager<ApplicationUser>signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }

        #region Login.

        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _accountService.ValidateUserAsync(model);
            if (user == null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid email or password.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index), "Home");

            if (result.IsNotAllowed)
                ModelState.AddModelError("InvalidLogin", "You are not allowed to login.");
            else if (result.IsLockedOut)
                ModelState.AddModelError("InvalidLogin", "Your account is locked out.");
            else
                ModelState.AddModelError("InvalidLogin", "Invalid email or password."); // ✅ wrong password

            return View(model);
        }

        #endregion

        #region Logout.

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login),"Account");
        }



        #endregion

        #region AccessDenied.

        public IActionResult AccessDenied()
        {
            return View();
        }

        #endregion



    }
}
