using InterrogateMe.Core.Data;
using InterrogateMe.Core.Data.Specification;
using InterrogateMe.Core.Models;
using InterrogateMe.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace InterrogateMe.Web.Pages
{
    public class IndexModel : PageModel
    {
        #region Private Variables

        private readonly IRepository _repository;
        private readonly ILogger _logger;

        #endregion Private Variables

        #region Public Properties

        [BindProperty]
        public Topic Topic { get; set; }

        #endregion Public Properties

        public IndexModel(IRepository repository, ILogger<IndexModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string generatedUrl;
            if (!ModelState.IsValid)
                return Page();

            generatedUrl = GenerateValidUrl();
            if(generatedUrl == null)
                return StatusCode((int)HttpStatusCode.InternalServerError);

            _repository.Add(new Link
            {
                Topic = Topic,
                Url = generatedUrl
            });
            return RedirectToPage("Question", new { link = generatedUrl });
        }

        #region Helper Method

        private string GenerateValidUrl()
        {
            var generatedUrl = string.Empty;

            try
            {
                for (int i = 0; i < 5; i++)
                {
                    generatedUrl = UrlHelper.GenerateUrl();
                    if (IsValidUrl(generatedUrl))
                        return generatedUrl;
                }
                throw new ArgumentException($"{generatedUrl} could not be generated");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"An error occured while generating a url {generatedUrl}", ex);
            }
            return generatedUrl;
        }

        private bool IsValidUrl(string generatedUrl)
        {
            var result = _repository.Single(LinkSpecification.ByUrl(generatedUrl));
            return result == null;
        }

        #endregion Helper Method
    }
}