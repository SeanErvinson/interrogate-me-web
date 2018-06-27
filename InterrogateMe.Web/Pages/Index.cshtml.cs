using System.Threading.Tasks;
using InterrogateMe.Core.Data;
using InterrogateMe.Core.Data.Specification;
using InterrogateMe.Core.Models;
using InterrogateMe.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterrogateMe.Web.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Topic Topic { get; set; }
        
        private readonly IRepository _repository;

        public IndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public void OnGet()
        {

        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            
            // Log In case generate has failed
            var generatedUrl = UrlHelper.GenerateUrl();

            if(!IsValidUrl(generatedUrl))
                return BadRequest();

            _repository.Add(new Link
            {
                Topic = Topic,
                Url = generatedUrl
            });

            return Redirect(generatedUrl);
        }

        private bool IsValidUrl(string generatedUrl)
        {
            var result = _repository.Single(LinkSpecification.ByUrl(generatedUrl));
            return result == null;
        }
    }
}
