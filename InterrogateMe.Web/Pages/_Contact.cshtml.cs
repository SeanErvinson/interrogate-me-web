using System.Net.Mail;
using System.Threading.Tasks;
using InterrogateMe.Core.Data;
using InterrogateMe.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace InterrogateMe.Web.Pages
{
    public class ContactModel : PageModel
    {

        #region Private Variables
        
        private ILogger _logger;

        #endregion

        //public ContactModel(IEmailService emailService, ILogger<ContactModel> logger)
        //{
        //    _emailService = emailService;
        //    _logger = logger;
        //}

        public void OnGet()
        {
        }

        //public async Task<IActionResult> OnPost()
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return Page();
        //    }


        //    try
        //    {
        //        await _emailService.SendEmailAsync(Sender);
        //    }
        //    catch (SmtpException ex)
        //    {
        //        _logger.LogError("Email was not successfully sent", ex);
        //        return BadRequest();
        //    }

        //    return Page();
        //}
    }
}
