using InterrogateMe.Core.Data;
using InterrogateMe.Core.Data.Specification;
using InterrogateMe.Core.Models;
using InterrogateMe.Utilities;
using InterrogateMe.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace InterrogateMe.Web.Pages
{
    public class QuestionModel : PageModel
    {
        #region Private Variable

        private readonly ILogger _logger;
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;

        #endregion Private Variable

        #region Public Properties

        [BindProperty]
        public Question Question { get; set; }

        [BindProperty]
        public string ShortcutLink { get; set; }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public bool ShowReCaptcha { get; set; }

        public InterrogateClient InterrogateClient { get; }


        #endregion Public Properties

        /// <summary>
        /// Candidate for refactoring. Implementing a IService or Service class in the middle
        /// that is triggered by an event
        /// </summary>
        public QuestionModel(IRepository repository, ILogger<QuestionModel> logger, IConfiguration configuration, InterrogateClient interrrogateClient)
        {
            _repository = repository;
            _logger = logger;
            _configuration = configuration;
            InterrogateClient = interrrogateClient;
        }

        public IActionResult OnGet(string link)
        {
            if (link == null)
            {
                return NotFound();
            }

            var resultLink = _repository.Single(LinkSpecification.ByUrl(link));
            if (resultLink == null)
            {
                return NotFound();
            }

            var resultTopic = _repository.Single(BaseSpecification<Topic>.ById(resultLink.TopicId));
            if (resultTopic == null)
            {
                return NotFound();
            }

            if (resultTopic.PreventSpam)
            {
                ShowReCaptcha = true;
            }

            Title = resultTopic.Title;
            ShortcutLink = link;
            return Page();
        }

        public IActionResult OnPost()
        {
            var resultLink = _repository.Single(LinkSpecification.ByUrl(ShortcutLink));

            if (resultLink == null)
                return NotFound();

            var resultTopic = _repository.SingleInclude(BaseSpecification<Topic>.ById(resultLink.TopicId), new List<ISpecification<Topic>> { TopicSpecification.IncludeQuestions() });

            if (resultTopic == null)
                return NotFound();

            if (resultTopic.PreventNSFW)
            {
                if (IsNsfw(Question.Content))
                    ModelState.AddModelError("Question.Content", "Question contains word/s that are considered as NSFW");
            }

            if (resultTopic.PreventSpam)
            {
                if(!ReCaptchaHelper.ValidateRecaptcha(Request.Form["g-recaptcha-response"]))
                    ModelState.AddModelError("Question.Content", "Invalid ReCaptcha");
            }

            switch (resultTopic.DuplicationCheck)
            {
                case DuplicationCheck.IpAddress:
                    {
                        if (IpAddressExists(WebHelper.GetRemoteIP, resultTopic.Id))
                        {
                            ModelState.AddModelError("Question.Content", "You have already asked a question on this poll");
                            break;
                        }
                        else if (ModelState.IsValid)
                            AddIpAddress(resultTopic.Id);
                        break;
                    }
                case DuplicationCheck.BrowserCookie:
                    {
                        const string cookieKey = "PID";
                        if (CookieExists(cookieKey, ShortcutLink))
                        {
                            ModelState.AddModelError("Question.Content", "You have already asked a question on this poll");
                            break;
                        }
                        else if(ModelState.IsValid)
                            WebHelper.SetCookie(cookieKey, ShortcutLink, null);
                        break;
                    }
                case DuplicationCheck.None: break;
                default:
                    _logger.LogError($"Unkown duplication check case {resultTopic.DuplicationCheck}.");
                    throw new InvalidEnumArgumentException("Unrecognized case value.");
            }

            if (!ModelState.IsValid)
                return Page();

            resultTopic.Questions.Add(Question);

            _repository.Update(resultTopic);

            InterrogateClient.UpdateList(ShortcutLink, Question);

            InterrogateClient.UpdateQuestionCount(ShortcutLink, resultTopic.Questions.Count);

            return RedirectToPage("Result");
        }

        private static bool CookieExists(string key, string value)
        {
            var cookieValue = WebHelper.GetCookie(key);
            if (cookieValue == null)
                return false;
            var cookies = cookieValue.Split("&");
            foreach (var cookie in cookies)
            {
                if (cookie == value)
                    return true;
            }
            return false;
        }

        private void AddIpAddress(Guid id)
        {
            _repository.Add(new IpAddress
            {
                Address = WebHelper.GetRemoteIP,
                UserAgent = WebHelper.GetUserAgent,
                RequestScheme = WebHelper.GetScheme,
                TopicId = id
            });
        }

        private bool IpAddressExists(string ipAddress, Guid topicId)
        {
            return _repository.Single(IpAddressSpecification.ByIpAddressAndTopicId(ipAddress, topicId)) != null;
        }

        private bool IsNsfw(string question)
        {
            const string pattern = @"\s+";
            var words = System.Text.RegularExpressions.Regex.Split(question.ToLower().Trim(), pattern);
            foreach (var word in words)
            {
                if (_repository.All(ProfaneWordSpecification.ByWord(word)).Count > 0)
                    return true;
            }
            return false;
        }
    }
}