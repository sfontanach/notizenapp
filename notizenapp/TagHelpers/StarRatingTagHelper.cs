using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace notizenapp.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("starrating")]
    public class StarRatingTagHelper : TagHelper
    {
        public string Rating { get; set; }
        public string Itemid { get; set; }
        public string disablecheck { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
			output.TagName = "span";

            string htmlContent = "";
            for (int i = 5; i > 0; i--) {
                string star;
                string strid = $"rating-input-1-{i}";
                string itemstarname = $"test1{Itemid}";
                string checkedString = (i.ToString() == Rating) ? "checked='checked'" : "";
                string disabledString = disablecheck == "true" ? "disabled" : "";
                star = $@"
                    <input {disabledString} type='radio' class='rating-input' 
                            id='{strid}' name='{itemstarname}' {checkedString}>
                    <label for='{strid}' class='rating-star'></label>";
                htmlContent += star;
            }

            output.Content.SetHtmlContent(htmlContent);
			output.TagMode = TagMode.StartTagAndEndTag;

        }
    }
}
