using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace InterrogateMe.Web.TagHelpers
{
    public class FacebookMetaTagHelper : TagHelper
    {
        /// <summary>
        /// Title of the topic
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Description of the topic
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Url for the image that is displayed on the card
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// Url of the topic
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Type of content
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Name of the site
        /// </summary>
        public string SiteName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.SuppressOutput();
            var sb = new StringBuilder();

            sb.AppendLine($"<meta property=\"og: url\" content=\"{Url}\">");
            sb.AppendLine($"<meta property=\"og: type\" content=\"{Type}\">");
            sb.AppendLine($"<meta property=\"og: title\" content=\"{Title}\">");
            sb.AppendLine($"<meta property=\"og: site_name\" content=\"{SiteName}\">");
            sb.AppendLine($"<meta property=\"og: description\" content=\"{Description}\">");
            sb.AppendLine($"<meta property=\"og: image\" content=\"{ImageUrl}\">");

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
