using System;
using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public class Question : BaseModel
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Content { get; set; }

        public DateTime DateAsked { get; set; } = DateTime.UtcNow;

        public int Like { get; set; }
    }
}
