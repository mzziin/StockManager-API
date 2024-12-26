using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Category;

namespace StockManager.BLL.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<ResponseModel<List<outCategoryDto>>> GetAllCategories();
        Task<ResponseModel<List<outSubcategoryDto>>> GetAllSubCategories(int categoryId);
        Task<ResponseModel<CategoryDto>> AddCategory(CategoryDto categoryDto);
    }
}