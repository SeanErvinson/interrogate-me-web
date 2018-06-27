using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterrogateMe.Core.Data;
using InterrogateMe.Core.Data.Specification;
using InterrogateMe.Core.Models;
using InterrogateMe.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterrogateMe.Web.Pages
{
    public class QuestionModel : PageModel
    {
        #region Private Variable

        private readonly IRepository _repositroy;

        #endregion

        #region Public Properties

        [BindProperty]
        public string TempLink { get; set; }
        [BindProperty]
        public Question Question { get; set; }
        public InterrogateClient InterrogateClient { get; }
        public string Title { get; set; }
        public string ShortcutLink { get; set; }

        #endregion
        /// <summary>
        /// Candidate for refactoring. Implementing a IService or Service class in the middle 
        /// that is triggered by an event
        /// </summary>
        public QuestionModel(IRepository repository, InterrogateClient interrrogateClient)
        {
            _repositroy = repository;
            this.InterrogateClient = interrrogateClient;
        }
        
        // Consider changing the PK of topic to the url of the link
        // So that when accessing the database I can search with ById(link.id)
        public async Task OnGet(string link)
        {
            var resultLink = _repositroy.Single(LinkSpecification.ByUrl(link));
            var resultTopic = _repositroy.Single(BaseSpecification<Topic>.ById(resultLink.TopicId));

            Title = resultTopic.Title;
            ShortcutLink = link;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var x = _repositroy.All(BaseSpecification<Topic>.ById(new Guid("s")));
            var g = _repositroy.Include<Topic>(y => y.Questions);
            InterrogateClient.UpdateList(TempLink, Question);

            return RedirectToPage("Result");
        }
    }
}