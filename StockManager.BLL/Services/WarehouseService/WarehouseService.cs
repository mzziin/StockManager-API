using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Product;
using StockManager.DAL.Repositories;

namespace StockManager.BLL.Services.WarehouseService
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WarehouseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<List<outProductDto>>> GetAllProductsFromWarehouse(
            int warehouseId,
            int? subcategoryId,
            string productName,
            int pageIndex,
            int pageSize
            )
        {
            var response = await _unitOfWork.ProductWarehouses.GetAllProductsFromWarehouse(
                warehouseId,
                subcategoryId,
                productName,
                pageIndex,
                pageSize
                );
            if (response == null)
            {
                return new ResponseModel<List<outProductDto>>
                {
                    Status = false,
                    Message = "No product found"
                };
            }
            return new ResponseModel<List<outProductDto>>
            {
                Status = true,
                Data = response.Select(p => new outProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()
            };
        }
    }
}
