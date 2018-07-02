using InterrogateMe.Core.Models;
using System.Threading.Tasks;

namespace InterrogateMe.Core.Data
{
    public interface IEmailService
    {
        Task SendEmailAsync(Sender sender);
    }
}
