using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace try22.Inferastructure
{
    public class ClaimsProvider : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
            if (identity.IsAuthenticated)
            {
                if (identity.Name=="a@a")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Gender, "male"));
                }
                else
                {
                    identity.AddClaim(new Claim(ClaimTypes.Gender, "female"));
                }
             
            }
            return Task.FromResult(principal);
        }
    }
}
