using InterrogateMe.Core.Data;
using InterrogateMe.Core.Data.Specification;
using InterrogateMe.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace InterrogateMe.Web.Pages
{
    public class ResultModel : PageModel
    {
        #region Private Variable

        private readonly IRepository _repository;
        private readonly ILogger _logeger;

        #endregion

        #region Public Properties

        public IEnumerable<Question> Questions { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }

        #endregion

        public ResultModel(IRepository repository, ILogger<ResultModel> logger)
        {
            _repository = repository;
            _logeger = logger;
        }

        public IActionResult OnGet(string link)
        {
            if (link == null)
                return NotFound();

            var resultLink = _repository.Single(LinkSpecification.ByUrl(link));
            if (resultLink == null)
                return NotFound();

            var resultTopic = _repository.SingleInclude(BaseSpecification<Topic>.ById(resultLink.TopicId), new List<ISpecification<Topic>> { TopicSpecification.IncludeQuestions() });
            if (resultTopic == null)
                return NotFound();

            Questions = resultTopic.Questions;
            Link = link;
            Title = resultTopic.Title;

            return Page();
        }
    }
}