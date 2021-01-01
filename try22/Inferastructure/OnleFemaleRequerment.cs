using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace try22.Inferastructure
{
    public class OnleFemaleRequerment:IAuthorizationRequirement
    {
        public string Name { get; set; }
    }
    public class OnleFemaleRequermentHandler : AuthorizationHandler<OnleFemaleRequerment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnleFemaleRequerment requirement)
        {
            if (context.User.Identity.Name==requirement.Name)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
