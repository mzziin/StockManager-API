using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Category;
using StockManager.DAL.Entities;
using StockManager.DAL.Repositories;

namespace StockManager.BLL.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<object>> AddCategory(CategoryDto categoryDto)
        {
            var category = new Category { CategoryName = categoryDto.CategoryName };
            var status = await _unitOfWork.Categories.InsertAsync(category);
            await _unitOfWork.SaveAsync();
            if (status)
                return new ResponseModel<object>
                {
                    Status = status,
                    Message = "category created successfully"
                };
            return new ResponseModel<object>
            {
                Status = status,
                Message = "Something went wrong"
            };
        }

        public async Task<ResponseModel<object>> UpdateCategory(int categoryId, CategoryDto categoryDto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category == null)
                return new ResponseModel<object>
                {
                    Status = true,
                    Message = "Category not found"
                };

            category.CategoryName = string.IsNullOrEmpty(categoryDto.CategoryName) ? category.CategoryName : categoryDto.CategoryName;
            var status = _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveAsync();
            if (!status)
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = "Something went wrong"
                };

            return new ResponseModel<object>
            {
                Status = true,
                Message = "category updated successfully"
            };
        }

        public async Task<ResponseModel<object>> DeleteCategory(int categoryId)
        {
            var result = await _unitOfWork.Categories.Delete(categoryId);
            await _unitOfWork.SaveAsync();

            if (!result)
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = "Something went wrong"
                };
            return new ResponseModel<object>
            {
                Status = true,
                Message = "Category deleted successfully"
            };
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


        public async Task<ResponseModel<List<outSubcategoryDto>>> GetAllSubcategories(int subcategoryId)
        {
            var dbSubcategories = await _unitOfWork.Subcategories.GetAllByExpressionAsync(c => c.CategoryId == subcategoryId);
            if (dbSubcategories == null || !dbSubcategories.Any())
            {
                return new ResponseModel<List<outSubcategoryDto>>
                {
                    Status = false,
                    Message = $"No subcategories found for category ID: {subcategoryId}",
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

        public async Task<ResponseModel<object>> AddSubcategory(SubcategoryDto subcategoryDto, int categoryId)
        {
            var subcategory = new Subcategory { SubcategoryName = subcategoryDto.SubcategoryName, CategoryId = categoryId };
            var status = await _unitOfWork.Subcategories.InsertAsync(subcategory);
            await _unitOfWork.SaveAsync();
            if (status)
                return new ResponseModel<object>
                {
                    Status = status,
                    Message = "Subcategory created successfully"
                };
            return new ResponseModel<object>
            {
                Status = status,
                Message = "Something went wrong"
            };
        }

        public async Task<ResponseModel<object>> UpdateSubcategory(int subcategoryId, SubcategoryDto subcategoryDto)
        {
            var subcategory = await _unitOfWork.Subcategories.GetByIdAsync(subcategoryId);
            if (subcategory == null)
                return new ResponseModel<object>
                {
                    Status = true,
                    Message = "subcategory not found"
                };

            subcategory.SubcategoryName = string.IsNullOrEmpty(subcategoryDto.SubcategoryName) ? subcategory.SubcategoryName : subcategoryDto.SubcategoryName;
            var status = _unitOfWork.Subcategories.Update(subcategory);
            await _unitOfWork.SaveAsync();
            if (!status)
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = "Something went wrong"
                };

            return new ResponseModel<object>
            {
                Status = true,
                Message = "Subcategory updated successfully"
            };
        }

        public async Task<ResponseModel<object>> DeleteSubcategory(int subcategoryId)
        {
            var result = await _unitOfWork.Subcategories.Delete(subcategoryId);
            await _unitOfWork.SaveAsync();

            if (!result)
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = "Something went wrong"
                };
            return new ResponseModel<object>
            {
                Status = true,
                Message = "Subcategory deleted successfully"
            };
        }
    }
}
