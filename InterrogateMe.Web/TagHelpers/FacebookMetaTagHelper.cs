using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace InterrogateMe.Web.TagHelpers
{
    public class FacebookMetaTagHelper : TagHelper
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
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
