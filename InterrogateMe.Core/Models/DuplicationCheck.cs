using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public enum DuplicationCheck
    {
        [Display(Name = "No Duplication Checking")]
        None = 0,
        [Display(Name = "Ip Duplication Checking")]
        IpCheck = 1 << 0,
        [Display(Name = "Browser Cookie Duplication Checking")]
        CookieCheck = 1 << 1
    }
}
