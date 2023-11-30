using BusinessLogic.Services;
using DB;

namespace BusinessLogic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddSingleton<FinistDBContext, FinistDBContext>(serviceProvider =>
            {
                return new FinistDBContext(config.GetConnectionString("DefaultConnection")!);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<AuthService>();
            app.MapGrpcService<UserInfoService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}