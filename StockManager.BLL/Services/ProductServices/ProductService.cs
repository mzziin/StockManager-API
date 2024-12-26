using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Product;
using StockManager.DAL.Entities;
using StockManager.DAL.Repositories;

namespace StockManager.BLL.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task<ResponseModel<object>> SellProduct(int productId, int warehouseId, int quantity, Guid customerId)
        {
            // Check if the product exists in the database
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product is null)
            {
                return new ResponseModel<object>()
                {
                    Status = false,
                    Message = "No product found"
                };
            }

            // Check if the warehouse exists in the database
            var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(warehouseId);
            if (warehouse is null)
                return new ResponseModel<object>()
                {
                    Status = false,
                    Message = "Warehouse not found"
                };

            // Check if the customer exists in the database
            var customer = await _unitOfWork.Customers.GetByExpressionAsync(c => c.CustomerId == customerId);
            if (customer is null)
                return new ResponseModel<object>()
                {
                    Status = false,
                    Message = "Customer not found, Add customer to database"
                };

            // Check wheather the product is available in the warehouse
            var warehouseProduct = await _unitOfWork.ProductWarehouses.GetProductFromWarehouse(productId, warehouseId);
            if (warehouseProduct is null)
                return new ResponseModel<object>()
                {
                    Status = false,
                    Message = "Product is not available in your warehouse"
                };

            // Check the stock of the product in the specified warehouse
            var warehouseStock = await _unitOfWork.ProductWarehouses.GetStockAsync(productId, warehouseId);
            if (warehouseStock < quantity || warehouseStock == 0)
            {
                return new ResponseModel<object>
                {
                    Status = false,
                    Message = "Insufficient stock in the warehouse"
                };
            }

            // Begin a transaction to ensure all operations are performed atomically
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Update the ProductWarehouse table to reflect the product sale (reduce stock)
                var status = await _unitOfWork.ProductWarehouses.DecrementProductQuantity(productId, warehouseId, quantity);
                if (!status)
                {
                    return new ResponseModel<object>
                    {
                        Status = false,
                        Message = "Failed to update warehouse stock"
                    };
                }

                // Create and store a new transaction ID to a variable for further use in sale table
                Guid newTransactionId = Guid.NewGuid();

                // Create and store a new sale ID to a variable for further use in ProductSale table
                Guid newSaleId = Guid.NewGuid();

                // Insert transaction record
                await _unitOfWork.Transactions.InsertAsync(new Transaction
                {
                    TransactionId = newTransactionId,
                    TransactionType = "sale",
                    Quantity = quantity,
                    TransactionDateTime = DateTime.Now,
                    WarehouseId = warehouseId,
                    RelatedEntityId = newSaleId
                });

                // Insert sale record
                await _unitOfWork.Sales.InsertAsync(new Sale
                {
                    SaleId = newSaleId,
                    TransactionId = newTransactionId,
                    CustomerId = customerId,
                    Quantity = quantity,
                    SaleDateTime = DateTime.Now,
                    TotalAmount = quantity * product.Price,
                    UnitPrice = product.Price,
                    WarehouseId = warehouseId,
                });

                // Insert product-sale relationship
                await _unitOfWork.ProductSales.InsertAsync(new ProductSale
                {
                    ProductId = productId,
                    SaleId = newSaleId,
                    Quantity = quantity
                });

                // Save and commit
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitAsync();

                return new ResponseModel<object>
                {
                    Status = true,
                    Message = $"sold {product.ProductName}, Qty: {quantity}"
                };
            }
            catch (Exception ex)
            {
                // Roll back the transaction if any error occurs
                await _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task<ResponseModel<object>> PurchaseProduct(int productId, int warehouseId, int quantity, Guid supplierId)
        {
            // Check if the product exists in the database
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product is null)
                return new ResponseModel<object>()
                {
                    Status = false,
                    Message = "Product not found"
                };

            // Check if the warehouse exists in the database
            var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(warehouseId);
            if (warehouse is null)
                return new ResponseModel<object>()
                {
                    Status = false,
                    Message = "Warehouse not found"
                };

            // Check if the supplier exists in the database
            var supplier = await _unitOfWork.Suppliers.GetByExpressionAsync(s => s.SupplierId == supplierId);
            if (supplier is null)
                return new ResponseModel<object>()
                {
                    Status = false,
                    Message = "Supplier not found, Add supplier to Database"
                };

            // Begin a transaction to ensure all operations are performed atomically
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Check wheather product available in warehouse
                var warehouseProduct = await _unitOfWork.ProductWarehouses.GetProductFromWarehouse(productId, warehouseId);
                if (warehouseProduct is null)
                    await _unitOfWork.ProductWarehouses.InsertAsync(new ProductWarehouse
                    {
                        ProductId = productId,
                        WarehouseId = warehouseId,
                        StockQuantity = 0
                    });

                // Increment product quantity in warehouse
                var status = await _unitOfWork.ProductWarehouses.IncrementProductQuantity(productId, warehouseId, quantity);
                if (!status)
                {
                    await _unitOfWork.RollBack();
                    return new ResponseModel<object>
                    {
                        Status = false,
                        Message = "Failed to update warehouse stock"
                    };
                }

                // Create and store a new transaction ID to a variable for further use in purchase table
                var newTransactionId = Guid.NewGuid();

                // Create and store a new purchase ID to a variable for further use in productpurchase table
                var newPurchaseId = Guid.NewGuid();

                // Insert transaction record
                await _unitOfWork.Transactions.InsertAsync(new Transaction
                {
                    TransactionId = newTransactionId,
                    TransactionType = "purchase",
                    Quantity = quantity,
                    RelatedEntityId = newPurchaseId,
                    TransactionDateTime = DateTime.Now,
                    WarehouseId = warehouseId
                });

                // Insert purchase record
                await _unitOfWork.Purchases.InsertAsync(new Purchase
                {
                    PurchaseId = newPurchaseId,
                    TransactionId = newTransactionId,
                    WarehouseId = warehouseId,
                    PurchaseDateTime = DateTime.Now,
                    Quantity = quantity,
                    SupplierId = supplierId,
                    UnitPrice = product.Price,
                    TotalAmount = product.Price * quantity,
                });

                // Insert product-purchase relationship
                await _unitOfWork.ProductPurchases.InsertAsync(new ProductPurchase
                {
                    ProductId = productId,
                    PurchaseId = newPurchaseId,
                    Quantity = quantity
                });

                // Save and commit
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitAsync();

                return new ResponseModel<object>
                {
                    Status = true,
                    Message = $"Bought {product.ProductName}, Qty: {quantity}"
                };
            }
            catch (Exception ex)
            {
                // Roll back the transaction if any error occurs
                await _unitOfWork.RollBack();
                throw;
            }

        }
    }
}
