using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public enum DuplicationCheck
    {
        [Display(Name = "No Duplication Checking")]
        None = 0,
        [Display(Name = "Ip Duplication Checking")]
        IpAddress = 1 << 0,
        [Display(Name = "Browser Cookie Duplication Checking")]
        BrowserCookie = 1 << 1
    }
}
