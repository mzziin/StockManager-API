using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Category;

namespace StockManager.BLL.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<ResponseModel<List<outCategoryDto>>> GetAllCategories();
        Task<ResponseModel<outCategoryDto>> AddCategory(CategoryDto categoryDto);
        Task<ResponseModel<object>> UpdateCategory(int categoryId, CategoryDto categoryDto);
        Task<ResponseModel<object>> DeleteCategory(int categoryId);

        Task<ResponseModel<List<outSubcategoryDto>>> GetAllSubcategories(int subcategoryId);
        Task<ResponseModel<outSubcategoryDto>> AddSubcategory(SubcategoryDto subcategoryDto, int categoryId);
        Task<ResponseModel<object>> UpdateSubcategory(int subcategoryId, SubcategoryDto subcategoryDto);
        Task<ResponseModel<object>> DeleteSubcategory(int subcategoryId);
    }
}