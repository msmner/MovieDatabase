namespace MovieDatabase.Web.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using MovieDatabase.Data.Models;

    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message, string movieTitle)
        {
            var connectionId = this.Context.ConnectionId;
            await this.Groups.AddToGroupAsync(connectionId, movieTitle);
            await this.Clients.Group(movieTitle).SendAsync(
                "NewMessage",
                new Message { User = this.Context.User.Identity.Name, Text = message, });
        }
    }
}
