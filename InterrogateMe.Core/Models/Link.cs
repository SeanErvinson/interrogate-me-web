using InterrogateMe.Core.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public class Link : BaseModel
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
