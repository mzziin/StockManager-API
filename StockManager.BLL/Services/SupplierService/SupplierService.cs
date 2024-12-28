using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Supplier;
using StockManager.DAL.Entities;
using StockManager.DAL.Repositories;

namespace StockManager.BLL.Services.SupplierService
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<List<outSupplierDto>>> GetAllSuppliers()
        {
            var result = await _unitOfWork.Suppliers.GetAllAsync();
            if (result == null)
                return new ResponseModel<List<outSupplierDto>>()
                {
                    Status = false,
                    Message = "No supplier found"
                };

            return new ResponseModel<List<outSupplierDto>>
            {
                Status = true,
                Data = result.Select(c => new outSupplierDto
                {
                    SupplierId = c.SupplierId,
                    SupplierName = c.SupplierName,
                    PhoneNumber = c.PhoneNumber
                }).ToList()
            };
        }

        public async Task<ResponseModel<outSupplierDto>> GetSupplierById(Guid suppierId)
        {
            var result = await _unitOfWork.Suppliers.GetByIdAsync(suppierId);
            if (result == null)
                return new ResponseModel<outSupplierDto>()
                {
                    Status = false,
                    Message = "Supplier not found"
                };

            return new ResponseModel<outSupplierDto>
            {
                Status = true,
                Data = new outSupplierDto
                {
                    SupplierId = result.SupplierId,
                    SupplierName = result.SupplierName,
                    PhoneNumber = result.PhoneNumber
                }
            };
        }

        public async Task<ResponseModel<object>> CreateSupplier(addSupplierDto addSupplierDto)
        {
            var result = await _unitOfWork.Suppliers.InsertAsync(new Supplier
            {
                SupplierName = addSupplierDto.SupplierName,
                PhoneNumber = addSupplierDto.PhoneNumber,
            });
            await _unitOfWork.SaveAsync();

            if (result is false)
                return new ResponseModel<object>()
                {
                    Status = false,
                    Message = "Somethong went wrong"
                };
            return new ResponseModel<object>()
            {
                Status = true,
                Message = "Supplier added successfully"
            };
        }

        public async Task<ResponseModel<object>> UpdateSupplier(Guid supplierId, editSupplierDto editSupplierDto)
        {
            var supplier = await _unitOfWork.Suppliers.GetByExpressionAsync(c => c.SupplierId == supplierId);

            if (supplier is null)
                return new ResponseModel<object>()
                {
                    Status = false,
                    Message = "supplier not found"
                };
            supplier.SupplierName = string.IsNullOrEmpty(editSupplierDto.SupplierName) ? supplier.SupplierName : editSupplierDto.SupplierName;
            supplier.PhoneNumber = string.IsNullOrEmpty(editSupplierDto.PhoneNumber) ? supplier.PhoneNumber : editSupplierDto.PhoneNumber;

            var result = _unitOfWork.Suppliers.Update(supplier);
            await _unitOfWork.SaveAsync();

            if (result is false)
                return new ResponseModel<object>()
                {
                    Status = false,
                    Message = "can't update customer"
                };

            return new ResponseModel<object>()
            {
                Status = true,
                Message = "Supplier updated successfully"
            };
        }

        public async Task<ResponseModel<object>> DeleteSupplier(Guid supplierId)
        {
            var result = await _unitOfWork.Suppliers.Delete(supplierId);
            if (result is false)
                return new ResponseModel<object>()
                {
                    Status = false,
                    Message = "something went wrong"
                };
            return new ResponseModel<object>()
            {
                Status = true,
                Message = "Supplier deleted successfully"
            };
        }
    }
}
