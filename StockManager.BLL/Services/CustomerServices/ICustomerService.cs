﻿using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Customer;

namespace StockManager.BLL.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<ResponseModel<List<outCustomerDto>>> GetAllCustomers();
        Task<ResponseModel<outCustomerDto>> GetCustomerById(Guid customoerId);
        Task<ResponseModel<outCustomerDto>> CreateCustomer(addCustomerDto addCustomerDto);
        Task<ResponseModel<object>> DeleteCustomer(Guid customerId);
        Task<ResponseModel<object>> UpdateCustomer(Guid customerId, editCustomerDto editCustomerDto);
    }
}