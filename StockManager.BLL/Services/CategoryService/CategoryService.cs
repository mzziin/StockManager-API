using StockManager.BLL.ApiModels;
using StockManager.DAL.Entities;
using StockManager.DAL.Repositories;

namespace StockManager.BLL.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<List<Category>>> GetAllCategories()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            if (categories.Any())
            {
                return new ResponseModel<List<Category>>
                {
                    Status = true,
                    Message = "categories fetched successfully",
                    Data = categories.ToList()
                };
            }
            return new ResponseModel<List<Category>>
            {
                Status = false,
                Message = "No categories found",
            };
        }

        public async Task<ResponseModel<List<Category>>> GetAllSubCategories(int categoryId)
        {
            var subCategories = await _unitOfWork.Categories.GetAllSubCategories(categoryId);
            if (subCategories.Any())
            {
                return new ResponseModel<List<Category>>
                {
                    Status = true,
                    Message = "subcategories fetched successfully",
                    Data = subCategories.ToList()
                };
            }
            return new ResponseModel<List<Category>>
            {
                Status = false,
                Message = $"No subcategories found for category ID: {categoryId}",
            };
        }
    }
}
