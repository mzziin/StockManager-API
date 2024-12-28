using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Customer;
using StockManager.DAL.Entities;
using StockManager.DAL.Repositories;

namespace StockManager.BLL.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<List<outCustomerDto>>> GetAllCustomers()
        {
            var result = await _unitOfWork.Customers.GetAllAsync();
            if (result == null)
                return new ResponseModel<List<outCustomerDto>>()
                {
                    Status = false,
                    Message = "No customer found"
                };

            return new ResponseModel<List<outCustomerDto>>
            {
                Status = true,
                Data = result.Select(c => new outCustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                }).ToList()
            };
        }

        public async Task<ResponseModel<outCustomerDto>> GetCustomerById(Guid customoerId)
        {
            var result = await _unitOfWork.Customers.GetByIdAsync(customoerId);
            if (result == null)
                return new ResponseModel<outCustomerDto>()
                {
                    Status = false,
                    Message = "Customer not found"
                };

            return new ResponseModel<outCustomerDto>
            {
                Status = true,
                Data = new outCustomerDto
                {
                    CustomerId = result.CustomerId,
                    CustomerName = result.CustomerName,
                    PhoneNumber = result.PhoneNumber,
                }
            };
        }

        public async Task<ResponseModel<object>> CreateCustomer(addCustomerDto addCustomerDto)
        {
            var result = await _unitOfWork.Customers.InsertAsync(new Customer
            {
                CustomerName = addCustomerDto.CustomerName,
                PhoneNumber = addCustomerDto.PhoneNumber,
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
                Message = "Customer added successfully"
            };
        }

        public async Task<ResponseModel<object>> UpdateCustomer(Guid customerId, editCustomerDto editCustomerDto)
        {
            var customer = await _unitOfWork.Customers.GetByExpressionAsync(c => c.CustomerId == customerId);

            if (customer is null)
                return new ResponseModel<object>()
                {
                    Status = false,
                    Message = "Customer not found"
                };
            customer.CustomerName = string.IsNullOrEmpty(editCustomerDto.CustomerName) ? customer.CustomerName : editCustomerDto.CustomerName;
            customer.PhoneNumber = string.IsNullOrEmpty(editCustomerDto.PhoneNumber) ? customer.PhoneNumber : editCustomerDto.PhoneNumber;

            var result = _unitOfWork.Customers.Update(customer);
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
                Message = "Customer updated successfully"
            };
        }

        public async Task<ResponseModel<object>> DeleteCustomer(Guid customerId)
        {
            var result = await _unitOfWork.Customers.Delete(customerId);
            if (result is false)
                return new ResponseModel<object>()
                {
                    Status = false,
                    Message = "something went wrong"
                };
            return new ResponseModel<object>()
            {
                Status = true,
                Message = "Customer deleted successfully"
            };
        }
    }
}
