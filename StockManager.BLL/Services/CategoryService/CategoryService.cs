using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Category;
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

        public async Task<ResponseModel<List<outCategoryDto>>> GetAllCategories()
        {
            var dbCategories = await _unitOfWork.Categories.GetAllAsync();
            if (dbCategories == null || !dbCategories.Any())
            {
                return new ResponseModel<List<outCategoryDto>>
                {
                    Status = false,
                    Message = "No categories found",
                };
            }

            var categories = dbCategories.Select(p => new outCategoryDto
            {
                CategoryId = p.CategoryId,
                CategoryName = p.CategoryName
            }).ToList();

            return new ResponseModel<List<outCategoryDto>>
            {
                Status = true,
                Message = "categories fetched successfully",
                Data = categories
            };
        }

        public async Task<ResponseModel<List<outSubcategoryDto>>> GetAllSubCategories(int categoryId)
        {
            var dbSubcategories = await _unitOfWork.Subcategories.GetAllByExpressionAsync(c => c.CategoryId == categoryId);
            if (dbSubcategories == null || !dbSubcategories.Any())
            {
                return new ResponseModel<List<outSubcategoryDto>>
                {
                    Status = false,
                    Message = $"No subcategories found for category ID: {categoryId}",
                };
            }

            var subcategories = dbSubcategories.Select(s => new outSubcategoryDto
            {
                SubcategoryId = s.SubcategoryId,
                SubcategoryName = s.SubcategoryName
            });

            return new ResponseModel<List<outSubcategoryDto>>
            {
                Status = true,
                Message = "successfully fetched all subcategories",
                Data = subcategories.ToList()
            };

        }
    }
}
