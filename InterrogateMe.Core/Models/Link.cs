using System;
using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public class Link : BaseModel
    {
        [Required]
        public string Url { get; set; }

        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
