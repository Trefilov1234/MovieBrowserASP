using Microsoft.AspNetCore.Razor.TagHelpers;
using Movie.Models;

namespace Movie.TagHelpers
{
    public class PictureTagHelper:TagHelper
    {
        public Cinema Cinema { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "image";
            output.Attributes.Add("src", Cinema.Poster);
            output.Attributes.Add("class", "rounded");
            
        }
    }
}
