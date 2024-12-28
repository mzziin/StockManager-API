using Microsoft.Extensions.DependencyInjection;
using StockManager.BLL.Services.AuthServices;
using StockManager.BLL.Services.CategoryServices;
using StockManager.BLL.Services.CustomerService;
using StockManager.BLL.Services.ProductServices;
using StockManager.BLL.Services.SupplierService;
using StockManager.BLL.Services.WarehouseServices;
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
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISupplierService, SupplierService>();
        }
    }
}
