using Grpc.Core;
using DB;
namespace BusinessLogic.Services
{
    public class UserInfoService : UserInfo.UserInfoBase
    {
        private readonly FinistDBContext _finistDBContext;
        public UserInfoService(FinistDBContext finistDBContext)
        {
            _finistDBContext = finistDBContext;
        }

        public override Task<UserInfoReply> GetUserInfo(UserInfoRequest request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                DB.Entities.Client client = _finistDBContext.Clients.FirstOrDefault(p=>p.Number == request.Number)!;
                Console.WriteLine(client.Name);
                return new UserInfoReply
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