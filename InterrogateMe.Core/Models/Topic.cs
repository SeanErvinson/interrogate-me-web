using InterrogateMe.Core.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public class Topic : BaseModel
    {
        [Required(ErrorMessage ="Please enter a title for the Q and A poll.")]
        [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "Poll title must be longer than three characters.")]
        public string Title { get; set; }

        [Display(Name = "Allow Multiple Likes")]
        public bool AllowMultipleLikes { get; set; }

        [Display(Name = "Improve Spam Prevention")]
        public bool PreventSpam { get; set; }

        [Display(Name ="Prevent NSFW")]
        public bool PreventNSFW { get; set; }

        [EnumDataType(typeof(DuplicationCheck))]
        public DuplicationCheck DuplicationCheck { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
