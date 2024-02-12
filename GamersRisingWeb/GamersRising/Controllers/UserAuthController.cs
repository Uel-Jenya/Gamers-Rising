using GamersRising.Data;
using GamersRising.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamersRising.Controllers
{
    public class UserAuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UserAuthController(ApplicationDbContext context,
                                    UserManager<ApplicationUser> userManager,
                                    SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            model.LoginInValid = "true";

            string userName;


            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
                else
                {
                    userName = user.UserName;
                }
            
                var result = await _signInManager.PasswordSignInAsync(userName,
                                                                     model.Password,
                                                                     model.RememberMe,
                                                                     lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    model.LoginInValid = "";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login. Please chek your Credidentials");
                }


            }

            return PartialView("_UserLoginPartial", model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
            registrationModel.RegistrationInValid = "true";

            if (ModelState.IsValid)
            {
                ApplicationUser userIn = new ApplicationUser
                {

                    UserName = registrationModel.UserName,
                    FullName = registrationModel.FullName,
                    Email = registrationModel.Email,
                    PhoneNumber = registrationModel.PhoneNumber,
                    DateOfBirth = registrationModel.BirthDate

                };

                var result = await _userManager.CreateAsync(userIn, registrationModel.Password);
                if (result.Succeeded) 
                {
                    registrationModel.RegistrationInValid = "";

                    //await _signInManager.SignInAsync(user, isPersistent: false)
                    await _signInManager.SignInAsync(userIn, isPersistent: false);
                    return PartialView("_UserRegistrationPartial", registrationModel);
                
                }
                else
                {
                    var errors = result.Errors;
                    var message = string.Join(", ", errors);
                    ModelState.AddModelError("", message);
                    return PartialView("_UserRegistrationPartial", registrationModel);


                }

            }

            ModelState.AddModelError("", "Registration Attempt faild");
            return PartialView("_UserRegistrationPartial", registrationModel);

        }

        public async Task<bool> UsernameExists(string name)
        {
            bool userName = await _context.Users.AnyAsync(u => u.UserName.ToUpper() == name.ToUpper());

            if (userName)
            {
                return true;
            }
            else
            {
                return false;
            }



        
        }

    }
}
