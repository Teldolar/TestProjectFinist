using Grpc.Core;
using DB;

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
                bool isExist = _finistDBContext.Clients.Any(p=>p.Number == request.Number && p.Password == request.Password);
                return new LoginReply
                {
                    IsExist= isExist,
                };
            });
        }
    }
}