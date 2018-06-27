using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public class Question : BaseModel
    {
        [Required]
        public string Content { get; set; }

        public int Like { get; set; }
    }
}
