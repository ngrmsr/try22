using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace try22.Inferastructure
{
    public class CustomIdentityError:IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
        {
            return new IdentityError { Code=nameof(DefaultError),Description=$"hgbjhbgbjbj"};
        }
    }
}
