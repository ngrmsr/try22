using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using try22.Models;

namespace try22.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;

        public AccountController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() {ReturnUrl=returnUrl });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user =await userManager.FindByNameAsync(model.UserName);
                if (user!=null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(model.ReturnUrl??"/");
                    }
                }
                ModelState.AddModelError(nameof(model.UserName), "username or pass not valid");
            }
            return View();
        }
        public async Task<IActionResult> SinOut(string returnUrl)
        {
           await signInManager.SignOutAsync();
            return Redirect("/");
        }
        public IActionResult accessDenied()
        {
       
            return Content("hhhh");
        }
    }
}
