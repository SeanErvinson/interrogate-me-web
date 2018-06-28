﻿using InterrogateMe.Core.Data;
using InterrogateMe.Core.Data.Specification;
using InterrogateMe.Core.Models;
using InterrogateMe.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

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

        public async Task<IActionResult> OnPostAsync()
        {
            string generatedUrl;
            if (!ModelState.IsValid)
                return Page();

            try
            {
                generatedUrl = GenerateValidUrl();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"There was a problem generating", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

            _repository.Add(new Link
            {
                Topic = Topic,
                Url = generatedUrl
            });
            return Redirect(generatedUrl);
        }

        #region Helper Method

        private string GenerateValidUrl()
        {
            var generatedUrl = UrlHelper.GenerateUrl();
            if (IsValidUrl(generatedUrl))
                return generatedUrl;
            throw new ArgumentException("Could not produce a valid url");
        }

        private bool IsValidUrl(string generatedUrl)
        {
            var result = _repository.Single(LinkSpecification.ByUrl(generatedUrl));
            return result == null;
        }

        #endregion Helper Method
    }
}