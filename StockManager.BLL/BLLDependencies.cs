using Microsoft.Extensions.DependencyInjection;
using StockManager.BLL.Services.AuthServices;
using StockManager.BLL.Services.CategoryService;
namespace StockManager.BLL
{
    public class BLLDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();

        }
    }
}
