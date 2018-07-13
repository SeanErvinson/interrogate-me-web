using InterrogateMe.Core.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public class Question : BaseModel
    {
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a question.")]
        [StringLength(int.MaxValue, MinimumLength = 2, ErrorMessage ="Question must be longer than two character")]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateAsked { get; set; } = DateTime.UtcNow;

        public int Like { get; set; }
    }
}
