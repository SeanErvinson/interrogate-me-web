using InterrogateMe.Core.Data;
using InterrogateMe.Core.Data.Specification;
using InterrogateMe.Core.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace InterrogateMe.Web.Pages
{
    public class ResultModel : PageModel
    {
        #region Private Variable

        private readonly IRepository _repository;

        #endregion

        #region Public Properties

        public IEnumerable<Question> Questions { get; set; }

        #endregion

        public ResultModel(IRepository repository)
        {
            _repository = repository;
        }

        public void OnGet(string link)
        {
            var resultLink = _repository.Single(LinkSpecification.ByUrl(link));
            var resultTopic = _repository.SingleInclude(BaseSpecification<Topic>.ById(resultLink.TopicId), new List<ISpecification<Topic>> { TopicSpecification.IncludeQuestions() });
            Questions = resultTopic.Questions;
        }
    }
}