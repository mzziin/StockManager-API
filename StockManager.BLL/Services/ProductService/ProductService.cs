using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Product;
using StockManager.DAL.Repositories;

namespace StockManager.BLL.Services.ProductService
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<List<outProductDto>>> GetAllProducts()
        {
            var dbProducts = await _unitOfWork.Products.GetAllAsync();
            if (dbProducts == null || !dbProducts.Any())
            {
                return new ResponseModel<List<outProductDto>>()
                {
                    Status = false,
                    Message = "No products found",
                    Data = new List<outProductDto>()
                };
            }

            var products = dbProducts.Select(p => new outProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                Quantity = p.Quantity,
                Price = p.Price
            }).ToList();

            return new ResponseModel<List<outProductDto>>()
            {
                Status = true,
                Message = "Products fetched successfully",
                Data = products
            };
        }
    }
}
