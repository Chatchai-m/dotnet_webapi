using Microsoft.AspNetCore.SignalR;
using System.Xml.Linq;

public class ChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    public async Task SendData(string sendName, string toName, string message)
    {
        Console.WriteLine($"sendName: {sendName}, toName: {toName}, message: {message}");
        await Clients.All.SendAsync("ReceiveMessage", sendName, toName, message);
    }
    //public async Task Send2(string sendName)
    //{
    //    await Clients.All.SendAsync("aaa", sendName);
    //}
}