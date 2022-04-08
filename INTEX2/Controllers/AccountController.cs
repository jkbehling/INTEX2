using INTEX2.Models;
using INTEX2.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Controller for Logging in

namespace INTEX2.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> um, SignInManager<IdentityUser> sim)
        {
            userManager = um;
            signInManager = sim;
        }

        // Get the page that takes them to the login form

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        // This is what we will send into the form to return

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Find the user associated with the passed in name
                IdentityUser user = await userManager.FindByNameAsync(loginModel.Username);

                //if (user.TwoFactorEnabled)
                //{
                //    return RedirectToAction("LoginTwoStep");
                //}

                if (user != null)
                {
                    await signInManager.SignOutAsync();

                    // Try logining in when password matches account password
                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Home/Admin");
                    }
                }
            }

            // Show when password is typed wrong
            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);

        }

        //[HttpGet]
        //public async Task<IActionResult> LoginTwoStep(string email, bool rememberMe, string returnUrl = null)
        //{
            
        //    var user = await userManager.FindByEmailAsync(email);
            
        //    var providers = await userManager.GetValidTwoFactorProvidersAsync(user);
            
        //    var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");
        //    var message = new Message(new string[] { email }, "Authentication token", token, null);
        //    await emailSender.SendEmailAsync(message);
        //    ViewData["ReturnUrl"] = returnUrl;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> LoginTwoStep(TwoStepModel twoStepModel, string returnUrl = null)
        //{
        //    return View();
        //}

        // This is so we can log out too

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();

            return Redirect(returnUrl);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser (model.Email);
                user.Email = model.Email;
                user.PhoneNumber = "384-987-5468";
                 
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach  (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                
                

               
            }
            return View(model);
        }

    }
}