using Microsoft.Extensions.DependencyInjection;
using StockManager.DAL.Repositories;
using StockManager.DAL.Repositories.AuthRepository;
using StockManager.DAL.Repositories.ProductWarehouseRepositories;
using StockManager.DAL.Repositories.TransactionRepositories;

namespace StockManager.DAL
{
    public class DALDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductWarehouseRepository, ProductWarehouseRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }
    }
}
