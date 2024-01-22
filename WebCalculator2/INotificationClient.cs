using SignalR_Common;

namespace WebCalculator2
{
    public interface INotificationClient
    {
        Task SendResult(double result);
        Task Send(Message message); 
        Task SendMessage(Message message);  
        Task IsExistsUserName(string userName);
        Task IsSavedUserName(string userName);
    }
}
