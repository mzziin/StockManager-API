using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Product;

namespace StockManager.BLL.Services.ProductService
{
    public interface IProductService
    {
        Task<ResponseModel<outProductDto>> CreateProduct(addProductDto addProductDto);
        Task<ResponseModel<object>> DeleteProduct(int productId);
        Task<ResponseModel<List<outProductDto>>> GetAllProducts();
        Task<ResponseModel<outProductDto>> GetProductById(int productId);
        Task<ResponseModel<outProductDto>> UpdateProduct(int productId, editProductDto editProductDto);
        Task<ResponseModel<object>> SellProduct(int productId, int warehouseId, int quantity, Guid customerId);
    }
}