using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace InterrogateMe.Web.TagHelpers
{
    public class TwitterMetaTagHelper : TagHelper
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Site { get; set; }
        public string Card { get; set; }
        public string Creator { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.SuppressOutput();
            var sb = new StringBuilder();

            sb.AppendLine($"<meta name=\"twitter: card\" content=\"{Card}\">");
            sb.AppendLine($"<meta name=\"twitter:site\" content=\"@{Site}\">");
            sb.AppendLine($"<meta name=\"twitter:creator\" content=\"@{Creator}\">");
            sb.AppendLine($"<meta name=\"twitter:title\" content=\"{Title}\">");
            sb.AppendLine($"<meta name=\"twitter:description\" content=\"{Description}\">");
            sb.AppendLine($"<meta name=\"twitter:image\" content=\"{ImageUrl}\">");

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
