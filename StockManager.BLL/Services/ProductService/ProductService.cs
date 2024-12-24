using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Product;
using StockManager.DAL.Entities;
using StockManager.DAL.Repositories;

namespace StockManager.BLL.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<outProductDto>> CreateProduct(addProductDto addProductDto)
        {
            var product = new Product
            {
                ProductName = addProductDto.ProductName,
                ProductDescription = addProductDto.ProductDescription,
                Price = addProductDto.Price,
                Quantity = addProductDto.Quantity,
                SubcategoryId = addProductDto.SubcategoryId,
            };
            var status = await _unitOfWork.Products.InsertAsync(product);
            await _unitOfWork.SaveAsync();
            if (status)
                return new ResponseModel<outProductDto>
                {
                    Status = true,
                    Message = "product added successfully",
                    Data = new outProductDto
                    {
                        ProductName = product.ProductName,
                        ProductDescription = product.ProductDescription,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        ProductId = product.ProductId,
                    }
                };
            return new ResponseModel<outProductDto>
            {
                Status = false,
                Message = "Something went wrong while adding",
            };
        }

        public async Task<ResponseModel<List<outProductDto>>> GetAllProducts()
        {
            var dbProducts = await _unitOfWork.Products.GetAllAsync();
            if (dbProducts == null || !dbProducts.Any())
            {
                return new ResponseModel<List<outProductDto>>()
                {
                    Status = false,
                    Message = "No products found",
                    Data = new List<outProductDto>()
                };
            }

            return new ResponseModel<List<outProductDto>>()
            {
                Status = true,
                Message = "Products fetched successfully",
                Data = dbProducts.Select(p => new outProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    Quantity = p.Quantity,
                    Price = p.Price
                }).ToList()
            };
        }

        public async Task<ResponseModel<outProductDto>> GetProductById(int productId)
        {
            var dbProduct = await _unitOfWork.Products.GetByIdAsync(productId);
            if (dbProduct == null)
            {
                return new ResponseModel<outProductDto>()
                {
                    Status = false,
                    Message = "No product found"
                };
            }

            return new ResponseModel<outProductDto>()
            {
                Status = true,
                Message = "Product fetched successfully",
                Data = new outProductDto
                {
                    ProductId = dbProduct.ProductId,
                    ProductName = dbProduct.ProductName,
                    ProductDescription = dbProduct.ProductDescription,
                    Quantity = dbProduct.Quantity,
                    Price = dbProduct.Price
                }
            };
        }

        public async Task<ResponseModel<outProductDto>> UpdateProduct(int productId, editProductDto editProductDto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product == null)
            {
                return new ResponseModel<outProductDto>()
                {
                    Status = false,
                    Message = "No product found"
                };
            }

            product.ProductName = string.IsNullOrEmpty(editProductDto.ProductName) ? product.ProductName : editProductDto.ProductName;
            product.ProductDescription = string.IsNullOrEmpty(editProductDto.ProductDescription) ? product.ProductDescription : editProductDto.ProductDescription;
            if (editProductDto.Quantity != null)
            {
                product.Quantity = (int)(editProductDto.Quantity < 0 ? product.Quantity : editProductDto.Quantity)!;
            }
            if (editProductDto.Price != null)
            {
                product.Price = (decimal)(editProductDto.Price < 0 ? product.Price : editProductDto.Price)!;
            }

            var status = _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveAsync();

            if (status)
                return new ResponseModel<outProductDto>
                {
                    Status = status,
                    Message = "product updated successfully",
                    Data = new outProductDto
                    {
                        ProductName = product.ProductName,
                        ProductDescription = product.ProductDescription,
                        Price = product.Price,
                        ProductId = product.ProductId,
                        Quantity = product.Quantity
                    }
                };
            else
                return new ResponseModel<outProductDto>()
                {
                    Status = false,
                    Message = "something went wrong"
                };
        }

        public async Task<ResponseModel<object>> DeleteProduct(int productId)
        {
            var status = await _unitOfWork.Products.Delete(productId);
            await _unitOfWork.SaveAsync();
            if (status)
                return new ResponseModel<object>
                {
                    Status = true,
                    Message = "successfully deleted product"
                };
            return new ResponseModel<object>
            {
                Status = false,
                Message = $"can't find product with id {productId}"
            };
        }
    }
}
