using Microsoft.AspNetCore.SignalR.Client;
using SignalR_Common;
using System.Xml.Linq;

namespace SignalR_Client
{
    internal class Program
    {
        static HubConnection hubConnection;


        static async Task Main(string[] args)
        {
            await InitSignalRClient();
            Console.WriteLine($"My connectionId : {hubConnection.ConnectionId}");

            bool isExistsUserName = true;
            while (isExistsUserName)
            {
                Console.WriteLine($"Enter your name");
                var userName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userName))
                {
                    Console.WriteLine("You have not entered your name", ConsoleColor.Red);
                    continue;
                }
                else
                {
                    isExistsUserName = false;
                    await hubConnection.SendAsync("SetMyName", userName);
                    Thread.Sleep(100);

                    bool isExit = false;

                    while (!isExit)
                    {
                        Console.WriteLine("Enter your message or command");

                        var userInput = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(userInput))                        
                            continue;
                        
                        if (userInput == "exit")                        
                            isExit = true;
                        
                        else
                        {
                            var message = new Message()
                            {
                                Title = $"New message from : {userName}--connectionId : {hubConnection.ConnectionId}",
                                Body = userInput
                            };

                            await hubConnection.SendAsync("SendMessage", message);
                            Console.WriteLine("Message send");
                        }
                    }
                }
            }
        }
        private static Task InitSignalRClient()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7217/notification")                
                .Build();

            hubConnection.On<Message>("Send", message =>
            {                
                Console.WriteLine($"Title: {message.Title}");
                Console.WriteLine($"Body: {message.Body}");
            });

            hubConnection.On<double>("SendResult", result =>
            {
                Console.WriteLine("New message from server");
                Console.WriteLine($"Result calculation = {result}");                
            });

            hubConnection.On<string>("IsExistsUserName", userName =>
            {
                Console.WriteLine("You have already entered your username");
                Console.WriteLine("Name not saved");
            });

            hubConnection.On<string>("IsSavedUserName", userName =>
                Console.WriteLine("Name saved")); 
            
            return hubConnection.StartAsync();
        }
    }
}