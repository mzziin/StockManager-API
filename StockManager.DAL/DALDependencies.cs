using Microsoft.Extensions.DependencyInjection;
using StockManager.DAL.Repositories.AuthRepository;

namespace StockManager.DAL
{
    public class DALDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
        }
    }
}
