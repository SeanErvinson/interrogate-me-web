using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text;

namespace InterrogateMe.Web.TagHelpers
{
    public class GoogleAdSenseTagHelper : TagHelper
    {
        public string AdClient { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.SuppressOutput();

            if (!String.IsNullOrEmpty(AdClient))
            {
                var sb = new StringBuilder();
                sb.AppendLine("<script async src=\"//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js\"></script>");
                sb.AppendLine("<script>");
                sb.AppendLine("     (adsbygoogle = window.adsbygoogle || []).push({");
                sb.AppendLine($"        google_ad_client: \"{AdClient}\",");
                sb.AppendLine("         enable_page_level_ads: true");
                sb.AppendLine("       }); ");
                sb.AppendLine("</script>");

                

            output.Content.SetHtmlContent(sb.ToString());
            }
        }
    }
}
