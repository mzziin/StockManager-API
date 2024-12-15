using Microsoft.Extensions.DependencyInjection;
using StockManager.BLL.Services;
using StockManager.BLL.Services.IServices;

namespace StockManager.BLL
{
    public class BLLDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

        }
    }
}
