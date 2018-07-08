using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace InterrogateMe.Web.TagHelpers
{
    public class TwitterMetaTagHelper : TagHelper
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
        /// Twitter handler of the site
        /// </summary>
        public string Site { get; set; }
        /// <summary>
        /// Type of card
        /// </summary>
        public string Card { get; set; }
        /// <summary>
        /// Twitter handler of the publisher
        /// </summary>
        public string Creator { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.SuppressOutput();
            var sb = new StringBuilder();

            sb.AppendLine($"<meta name=\"twitter:card\" content=\"{Card}\">");
            sb.AppendLine($"<meta name=\"twitter:site\" content=\"@{Site}\">");
            sb.AppendLine($"<meta name=\"twitter:creator\" content=\"@{Creator}\">");
            sb.AppendLine($"<meta name=\"twitter:title\" content=\"{Title}\">");
            sb.AppendLine($"<meta name=\"twitter:description\" content=\"{Description}\">");
            sb.AppendLine($"<meta name=\"twitter:image\" content=\"{ImageUrl}\">");

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
