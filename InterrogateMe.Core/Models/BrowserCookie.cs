using InterrogateMe.Core.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public class BrowserCookie : BaseIdentifier
    {
        [Required]
        public string Cookie { get; set; }
    }
}
