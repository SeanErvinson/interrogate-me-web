using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace InterrogateMe.Web.TagHelpers
{
    public class GooglePlusMetaTagHelper : TagHelper
    {
        /// <summary>
        /// Title of the topic
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description of the topic
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Url for the image that is displayed on the card
        /// </summary>
        public string ImageUrl { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.SuppressOutput();
            var sb = new StringBuilder();

            sb.AppendLine($"<meta itemprop=\"name\" content=\"{Name}\">");
            sb.AppendLine($"<meta itemprop=\"description\" content=\"{Description}\">");
            sb.AppendLine($"<meta itemprop=\"image\" content=\"{ImageUrl}\">");

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
