using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InterrogateMe.Core.Models
{
    public class Topic : BaseModel
    {
        [Required] public string Title { get; set; }

        public bool AllowMultipleLikes { get; set; }

        public bool PreventIpDuplication { get; set; }

        public bool AllowNsfw { get; set; }

        public IList<Question> Questions { get; set; }
    }
}
