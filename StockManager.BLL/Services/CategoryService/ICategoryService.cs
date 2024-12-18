using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Category;

namespace StockManager.BLL.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ResponseModel<List<outCategoryDto>>> GetAllCategories();
        Task<ResponseModel<List<outSubcategoryDto>>> GetAllSubCategories(int categoryId);
    }
}