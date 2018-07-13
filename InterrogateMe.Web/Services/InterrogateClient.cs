using InterrogateMe.Core.Models;
using InterrogateMe.Web.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace InterrogateMe.Web.Services
{
    public class InterrogateClient
    {
        private readonly IHubContext<InterrogateHub> _hubContext;

        public InterrogateClient(IHubContext<InterrogateHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void UpdateList(string groupName, Question newQuestion)
        {
            _hubContext.Clients.Group(groupName).SendAsync("OnNotifyList", newQuestion);
        }

        public void UpdateLike(string groupName)
        {
            _hubContext.Clients.Group(groupName).SendAsync("OnUpdateLike");
        }

        public void UpdateQuestionCount(string groupName, int count)
        {
            _hubContext.Clients.Group(groupName).SendAsync("OnUpdateQuestionCount", count);
        }
    }
}
