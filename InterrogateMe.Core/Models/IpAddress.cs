using InterrogateMe.Core.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public class IpAddress : BaseModel
    {
        [Required]
        public string Address { get; set; }

        public string RequestScheme { get; set; }

        public string UserAgent { get; set; }

        [Required]
        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }
    }
}
