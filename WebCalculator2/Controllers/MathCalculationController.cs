using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SignalR_Common;
using WebCalculator2;
using WebCalculator2.Hubs;
using Action = WebCalculator2.Entity.Action;


namespace WebCalculator2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathCalculationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub,INotificationClient> _hubContext;

        public MathCalculationController(IHubContext<NotificationHub,INotificationClient> _hubContext)
        {
            this._hubContext = _hubContext;
        }


        [HttpGet("/SendMathCalculationAllClients")]
        public async Task<double> GetAsync(double a, string action, double b)
        {
            using var dbContext = new ApplicationDbContext();
            Calculation calculation = new Calculation();
            Action action1 = new Action();

            action1.FirstNumber = a;
            action1.SecondNumber = b;
            action1.MathAction = action;
            action1.RegistrationDate = DateTime.Now;
            action1.Result = calculation.CreateCalculation(a, action, b);


            await dbContext.AddAsync(action1);
            await dbContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendResult(action1.Result);

           return action1.Result; 
        }

        [HttpGet("/SendMathCalculationSelectedClient")]
        public async Task<double> GetSelectedAsync(double a, string action, double b,string connectionId)
        {


            using var dbContext = new ApplicationDbContext();
            Calculation calculation = new Calculation();
            Action action1 = new Action();

            action1.FirstNumber = a;
            action1.SecondNumber = b;
            action1.MathAction = action;
            action1.RegistrationDate = DateTime.Now;
            action1.Result = calculation.CreateCalculation(a, action, b);


            await dbContext.AddAsync(action1);
            await dbContext.SaveChangesAsync();
            await _hubContext.Clients.Client(connectionId).SendResult(action1.Result);

            return action1.Result;

        }

        [HttpGet("/AllMathCalculation")]
        public async Task<List<Action>> GetAllActionAsync()
        {           

            using var dbContext = new ApplicationDbContext();           

            return await dbContext.Actions.ToListAsync();
        }

    }
}