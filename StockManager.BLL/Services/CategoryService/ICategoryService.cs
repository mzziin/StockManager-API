using StockManager.BLL.ApiModels;
using StockManager.DAL.Entities;

namespace StockManager.BLL.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ResponseModel<List<Category>>> GetAllCategories();
        Task<ResponseModel<List<Category>>> GetAllSubCategories(int categoryId);
    }
}