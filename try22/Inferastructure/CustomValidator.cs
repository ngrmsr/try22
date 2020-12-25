using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using try22.Models;

namespace try22.Inferastructure
{
    public class CustomValidator : /*IUserValidator<AppUser>*/ UserValidator<AppUser>
    {
        //public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        //{
        //    if (user.Email.ToLower().EndsWith("b"))
        //    {
        //        return Task.FromResult(IdentityResult.Failed(new IdentityError[]{
        //            new  IdentityError(){Code="",Description="hhhjhjhj"}
        //        }));
        //    }
        //    else
        //    {
        //        return Task.FromResult(IdentityResult.Success);
        //    }
        //}
        public override Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            if (user.Email.ToLower().EndsWith("b"))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError[]{
                    new  IdentityError(){Code="",Description="hhhjhjhj"}
                }));
            }
            else
            {
                return base.ValidateAsync(manager, user);
                //return Task.FromResult(IdentityResult.Success);
            }
        }
    }
}
