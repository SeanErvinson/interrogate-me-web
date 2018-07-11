using InterrogateMe.Core.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public class IpAddress : BaseIdentifier
    {
        [Required]
        public string Address { get; set; }
    }
}
