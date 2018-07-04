using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public class Topic : BaseModel
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "Must have minimum length of 3")]
        public string Title { get; set; }

        [Display(Name = "Allow Multiple Likes")]
        public bool AllowMultipleLikes { get; set; }

        [Display(Name = "Prevent Ip Duplication")]
        public bool PreventIpDuplication { get; set; }

        [Display(Name ="Prevent NSFW")]
        public bool PreventNSFW { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
