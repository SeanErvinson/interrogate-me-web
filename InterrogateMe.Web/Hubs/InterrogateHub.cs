using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace InterrogateMe.Web.Hubs
{
    public class InterrogateHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var groupName = Context.GetHttpContext().Request.Query["groupName"].SingleOrDefault();
            if (groupName != null)
                // Log : Every user should be expected to join a group
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await base.OnConnectedAsync();
        }
    }
}
