using Microsoft.AspNetCore.SignalR;

namespace dotnet_webapi.SignalR
{
    public class SignalrHub : Hub
    {
        public async Task NewMessage(string user, string message)
        {
            Console.WriteLine($"user: {user}, message: {message}");
            await Clients.All.SendAsync("messageReceived", user, message);
        }
    }
}
