using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using try22.Models;

namespace try22.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IPasswordHasher<AppUser> passwordHasher;
        private readonly IUserValidator<AppUser> userValidator;
        private readonly IPasswordValidator<AppUser> passwordValidator;

        public AdminController(UserManager<AppUser> userManager,IPasswordHasher<AppUser> passwordHasher
            ,IUserValidator<AppUser> userValidator,IPasswordValidator<AppUser>passwordValidator)
        {
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
            this.userValidator = userValidator;
            this.passwordValidator = passwordValidator;
        }
        public IActionResult Index()
        {
            var data = userManager.Users.ToList();
            List<UserViewModel> users = new List<UserViewModel>();
            foreach (var item in data)
            {
                users.Add(new UserViewModel() { 
                    Id=item.Id,
                UserName=item.UserName,
                Pass=item.PasswordHash,
                Email=item.Email
                });
            }
            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult res = await userManager.CreateAsync(user, model.Pass);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
          var d=  await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return View(new UserViewModel() { 
            Email=user.Email,
            Pass=user.PasswordHash,
            UserName=user.UserName,
            Id=user.Id
            
            });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                user.Email = model.Email;
                user.UserName = model.UserName;
                var userValid = await userValidator.ValidateAsync(userManager,user);
                if (!userValid.Succeeded)
                {
                    foreach (var item in userValid.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                else
                {
                    var passValid = await passwordValidator.ValidateAsync(userManager, user, model.Pass);
                    if (!passValid.Succeeded)
                    {
                        foreach (var item in passValid.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                    else
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, model.Pass);
                        IdentityResult res = await userManager.UpdateAsync(user);
                        if (res.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            foreach (var item in res.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                
                }
               
            }
            return View();
        }
    }
}
