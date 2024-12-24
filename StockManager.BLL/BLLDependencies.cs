using Microsoft.Extensions.DependencyInjection;
using StockManager.BLL.Services.AuthServices;
using StockManager.BLL.Services.CategoryService;
using StockManager.BLL.Services.ProductService;
using StockManager.BLL.Services.WarehouseService;
namespace StockManager.BLL
{
    public class BLLDependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
        }
    }
}
