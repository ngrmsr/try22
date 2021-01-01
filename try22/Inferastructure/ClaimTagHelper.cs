using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace try22.Inferastructure
{
    public class ClaimTagHelper : TagHelper
    {
        public IEnumerable<Claim> model { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "CustomTagHelper";
            string data=null;
            foreach (var item in model)
            {
                data += $"<th> {item.Subject} </th><th> {item.Issuer} </th><th> {item.Type} </th><th> {item.Value} </th>";
    }
            output.PreContent.SetHtmlContent(data);
        }

    }
}

