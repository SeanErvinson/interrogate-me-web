using System;
using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models.Base
{
    public class BaseIdentifier : BaseModel
    {
        public string RequestScheme { get; set; }

        public string UserAgent { get; set; }

        [Required]
        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }
    }
}
