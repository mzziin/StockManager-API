using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs;
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
            int pageSize)
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

        public async Task<ResponseModel<List<outTransaction>>> SearchTransactions(
            int warehouseId,
            string transactionType,
            string startDate,
            string endDate,
            int pageIndex,
            int pageSize)
        {
            if (transactionType.ToLower() != "sale" || transactionType.ToLower() != "purchase")
            {
                transactionType = null;
            }

            var result = await _unitOfWork.Transactions.GetAllTransactions(warehouseId, transactionType, startDate, endDate, pageIndex, pageSize);

            if (result == null)
                return new ResponseModel<List<outTransaction>>
                {
                    Status = false,
                    Message = "No result found"
                };

            return new ResponseModel<List<outTransaction>>
            {
                Status = true,
                Data = result.Select(t => new outTransaction
                {
                    TransactionId = t.TransactionId,
                    TransactionType = t.TransactionType,
                    Quantity = t.Quantity,
                    TransactionDateTime = t.TransactionDateTime,
                }).ToList()
            };
        }

        public async Task<ResponseModel<outTransaction>> GetTransactionById(int warehouseId, Guid transactionId)
        {
            var result = await _unitOfWork.Transactions.GetByExpressionAsync(t => t.TransactionId == transactionId && t.WarehouseId == warehouseId);
            if (result == null)
                return new ResponseModel<outTransaction>
                {
                    Status = false,
                    Message = "No result found"
                };

            return new ResponseModel<outTransaction>
            {
                Status = true,
                Data = new outTransaction
                {
                    TransactionId = result.TransactionId,
                    Quantity = result.Quantity,
                    TransactionType = result.TransactionType,
                    TransactionDateTime = result.TransactionDateTime
                }
            };
        }
    }
}
