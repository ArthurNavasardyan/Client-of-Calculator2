using Microsoft.AspNetCore.SignalR;
using SignalR_Common;
using System.Diagnostics;

namespace WebCalculator2.Hubs
{
    public class NotificationHub : Hub<INotificationClient>
    {
        public Task SendMessage(Message message)
        {            
            return Clients.Others.Send(message);
        }

        public Task SetMyName(string name)
        {
            if (Context.Items.ContainsKey("user_name"))                
                return Clients.Caller.IsExistsUserName(name);
            else
            {
                Context.Items.TryAdd("user_name", name);                
                return Clients.Caller.IsSavedUserName(name);
            }

        }
    }
}
