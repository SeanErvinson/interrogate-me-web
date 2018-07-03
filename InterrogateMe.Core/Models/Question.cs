using System;
using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public class Question : BaseModel
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 2, ErrorMessage ="Must have a minimum length of 2")]
        public string Content { get; set; }

        public DateTime DateAsked { get; set; } = DateTime.UtcNow;

        public int Like { get; set; }
    }
}
