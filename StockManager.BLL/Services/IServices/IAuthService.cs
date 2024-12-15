﻿using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs;

namespace StockManager.BLL.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseModel<object>> RegisterUserOrAdmin(RegisterModel registerModel, string role);
        Task<ResponseModel<UserDto>> LoginUser(LoginModel loginModel);
    }
}