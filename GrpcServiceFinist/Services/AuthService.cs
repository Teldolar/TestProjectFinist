using Grpc.Core;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using DB;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class AuthService : Auth.AuthBase
    {
        private readonly FinistDBContext _finistDBContext;
        public AuthService(FinistDBContext finistDBContext)
        {
            _finistDBContext = finistDBContext;
        }

        public override Task<LoginReply> Login(LoginRequest request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                DB.Entities.Client client = _finistDBContext.Clients.FirstOrDefault(p=>p.Number == request.Number && p.Password == request.Password) ?? new DB.Entities.Client()
                {
                    Name = "",
                    Number = "",
                    UrgentAccount = "",
                    DemandAccount = "",
                    CardAccount = ""
                };
                Console.WriteLine(client.Name);
                return new LoginReply
                {
                    ClientName = client.Name,
                    ClientNumber = client.Number,
                    UrgentAccount = client.UrgentAccount,
                    DemandAccount = client.DemandAccount,
                    CardAccount = client.CardAccount
                };
            });
        }
    }
}