using InterrogateMe.Core.Data;
using InterrogateMe.Core.Data.Specification;
using InterrogateMe.Core.Models;
using InterrogateMe.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterrogateMe.Web.Pages
{
    public class QuestionModel : PageModel
    {
        #region Private Variable

        private readonly IRepository _repository;

        #endregion Private Variable

        #region Public Properties

        [BindProperty]
        public string TempLink { get; set; }

        [BindProperty]
        public Question Question { get; set; }

        public InterrogateClient InterrogateClient { get; }
        public string Title { get; set; }
        public string ShortcutLink { get; set; }

        #endregion Public Properties

        /// <summary>
        /// Candidate for refactoring. Implementing a IService or Service class in the middle
        /// that is triggered by an event
        /// </summary>
        public QuestionModel(IRepository repository, InterrogateClient interrrogateClient)
        {
            _repository = repository;
            this.InterrogateClient = interrrogateClient;
        }

        // Consider changing the PK of topic to the url of the link
        // So that when accessing the database I can search with ById(link.id)
        public async Task OnGet(string link)
        {
            var resultLink = _repository.Single(LinkSpecification.ByUrl(link));
            var resultTopic = _repository.Single(BaseSpecification<Topic>.ById(resultLink.TopicId));

            Title = resultTopic.Title;
            ShortcutLink = link;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var resultLink = _repository.Single(LinkSpecification.ByUrl(TempLink));

            if (resultLink == null)
                return NotFound();

            var resultTopic = _repository.SingleInclude(BaseSpecification<Topic>.ById(resultLink.TopicId), new List<ISpecification<Topic>> { TopicSpecification.IncludeQuestions() });

            if (resultTopic == null)
                return NotFound();

            resultTopic.Questions.Add(Question);

            _repository.Update(resultTopic);

            InterrogateClient.UpdateList(TempLink, Question);

            return RedirectToPage("Result");
        }
    }
}