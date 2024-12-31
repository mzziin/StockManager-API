using Microsoft.EntityFrameworkCore;
using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs;
using StockManager.BLL.DTOs.Product;
using StockManager.BLL.DTOs.Warehouse;
using StockManager.DAL.Entities;
using StockManager.DAL.Repositories;
using StockManager.DAL.Repositories.AuthRepository;

namespace StockManager.BLL.Services.WarehouseServices
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthRepository _authRepository;
        public WarehouseService(IUnitOfWork unitOfWork, IAuthRepository authRepository)
        {
            _authRepository = authRepository;
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
            if (response is null)
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

            if (result is null)
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
            if (result is null)
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

        public async Task<ResponseModel<List<outWarehouseDto>>> GetAllWarehouses()
        {
            var result = await _unitOfWork.Warehouses.GetAllAsync();
            if (result is null)
                return new ResponseModel<List<outWarehouseDto>>
                {
                    Status = false,
                    Message = "No warehouses found"
                };
            return new ResponseModel<List<outWarehouseDto>>
            {
                Status = true,
                Data = result.Select(w => new outWarehouseDto
                {
                    WarehouseName = w.WarehouseName,
                    WarehouseLocation = w.WarehouseLocation,
                    WarehouseId = w.WarehouseId,
                    CreatedDateTime = w.CreatedDateTime,
                    IsActive = w.IsActive,
                    UpdatedDateTime = w.UpdatedDateTime
                }).ToList()
            };
        }

        public async Task<ResponseModel<outWarehouseDto>> GetWarehouseById(int warehouseId)
        {
            var result = await _unitOfWork.Warehouses.GetByIdAsync(warehouseId);
            if (result is null)
                return new ResponseModel<outWarehouseDto>
                {
                    Status = false,
                    Message = "Warehouse not found"
                };
            return new ResponseModel<outWarehouseDto>
            {
                Status = true,
                Data = new outWarehouseDto
                {
                    WarehouseName = result.WarehouseName,
                    WarehouseLocation = result.WarehouseLocation,
                    WarehouseId = result.WarehouseId,
                    CreatedDateTime = result.CreatedDateTime,
                    IsActive = result.IsActive,
                    UpdatedDateTime = result.UpdatedDateTime
                }
            };
        }

        public async Task<ResponseModel<object>> CreateWarehouse(addWarehouseDto addWarehouseDto)
        {
            var result = await _unitOfWork.Warehouses.InsertAsync(new Warehouse
            {
                WarehouseName = addWarehouseDto.WarehouseName,
                WarehouseLocation = addWarehouseDto.WarehouseLocation,
                CreatedDateTime = DateTime.Now
            });
            await _unitOfWork.SaveAsync();

            if (result is false)
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = "Warehouse not found"
                };
            return new ResponseModel<object>
            {
                Status = true,
                Message = "Warehouse added successfully"
            };
        }

        public async Task<ResponseModel<object>> UpdateWarehouse(int warehouseId, editWarehouseDto editWarehouseDto)
        {
            var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(warehouseId);
            if (warehouse is null)
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = "warehouse not found"
                };

            warehouse.WarehouseName = string.IsNullOrEmpty(editWarehouseDto.WarehouseName) ? warehouse.WarehouseName : editWarehouseDto.WarehouseName;
            warehouse.WarehouseLocation = string.IsNullOrEmpty(editWarehouseDto.WarehouseLocation) ? warehouse.WarehouseLocation : editWarehouseDto.WarehouseLocation;
            warehouse.UpdatedDateTime = DateTime.Now;

            bool result = _unitOfWork.Warehouses.Update(warehouse);
            await _unitOfWork.SaveAsync();

            if (!result)
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = "Something went wrong"
                };

            return new ResponseModel<object>
            {
                Status = true,
                Message = "warehouse updated successfully"
            };
        }

        public async Task<ResponseModel<object>> DeleteWarehouse(int warehouseId)
        {
            var result = await _unitOfWork.Warehouses.Delete(warehouseId);
            await _unitOfWork.SaveAsync();

            if (!result)
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = "Something went wrong"
                };

            return new ResponseModel<object>
            {
                Status = true,
                Message = "warehouse deleted successfully"
            };
        }

        public async Task<ResponseModel<object>> AssignManager(int warehouseId, Guid userId)
        {
            // Fetch warehouse and user entities
            var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(warehouseId);
            if (warehouse is null)
            {
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = $"No warehouse found with ID {warehouseId}"
                };
            }

            var user = await _authRepository.GetUserById(userId);
            if (user is null)
            {
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = $"No user found with ID {userId}"
                };
            }

            try
            {
                // Assign manager and update warehouse
                warehouse.WarehouseManagerId = user.Id;
                var status = _unitOfWork.Warehouses.Update(warehouse);

                await _unitOfWork.SaveAsync();
                if (status)
                    return new ResponseModel<object>
                    {
                        Status = true,
                        Message = $"{user.UserName} is assigned as the warehouse manager of {warehouse.WarehouseName}"
                    };
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = $"something went wrong: "
                };

            }
            catch (DbUpdateException dbEx)
            {
                // Log and handle other database-related exceptions
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = "A database error occurred. Please try again."
                };
            }
            catch (Exception ex)
            {
                // Handle generic exceptions
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }

        }
    }
}