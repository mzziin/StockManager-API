using Moq;
using StockManager.BLL.DTOs.Product;
using StockManager.BLL.Services.ProductServices;
using StockManager.DAL.Entities;
using StockManager.DAL.Repositories;

namespace StockManager.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ProductService _productService;
        public ProductServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _productService = new ProductService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = new Product { ProductId = productId, ProductName = "Test Product" };
            var mockProductRepository = new Mock<IGenericRepository<Product>>();

            mockProductRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(expectedProduct);

            _mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);

            // Act
            var result = await _productService.GetProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Data.ProductId);
            Assert.Equal("Test Product", result.Data.ProductName);
        }


        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnProducts_WhenProductExists()
        {
            // Arrange

            var expectedProduct = new List<Product>{
                new Product { ProductId = 1, ProductName = "Test Product 1" },
                new Product { ProductId = 2, ProductName = "Test Product 2" },
                new Product { ProductId = 3, ProductName = "Test Product 3" }
            };
            var mockProductRepository = new Mock<IGenericRepository<Product>>();

            mockProductRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(expectedProduct);

            _mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);

            // Act
            var result = await _productService.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.Count, result.Data.Count());
            Assert.Equal(expectedProduct[0].ProductId, result.Data.ElementAt(0).ProductId);
            Assert.Equal(expectedProduct[0].ProductName, result.Data.ElementAt(0).ProductName);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldReturnProduct()
        {
            // Arrange

            var addProductDto = new addProductDto
            {
                ProductName = "Test Product 1",
                Price = 345,
                SubcategoryId = 12,
                Quantity = 10,
                ProductDescription = "Lorem Ipsum"
            };
            var expectedProduct = new Product
            {
                ProductName = addProductDto.ProductName,
                Price = addProductDto.Price,
                SubcategoryId = addProductDto.SubcategoryId,
                Quantity = addProductDto.Quantity,
                ProductDescription = addProductDto.ProductDescription
            };

            var mockProductRepository = new Mock<IGenericRepository<Product>>();
            mockProductRepository.Setup(r => r.InsertAsync(It.IsAny<Product>()))
                 .ReturnsAsync((Product product) =>
                 {
                     product.ProductId = 1; // Assign an ID
                     return product;
                 });

            _mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);

            // Act
            var result = await _productService.CreateProduct(addProductDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.ProductName, result.Data.ProductName);
            Assert.Equal(expectedProduct.Price, result.Data.Price);
            Assert.Equal(expectedProduct.SubcategoryId, result.Data.SubcategoryId);
            Assert.Equal(expectedProduct.Quantity, result.Data.Quantity);
            Assert.Equal(expectedProduct.ProductDescription, result.Data.ProductDescription);
        }

        /*[Fact]
        public async Task GetProductByExpressionAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var productId = 2;
            var expectedProduct = new Product { ProductId = productId, ProductName = "Test Product 2" };
            var mockProductRepository = new Mock<IGenericRepository<Product>>();

            mockProductRepository
                .Setup(r => r.GetByExpressionAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(expectedProduct);

            _mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);

            // Act
            var result = await _productService.(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Data.ProductId);
            Assert.Equal("Test Product", result.Data.ProductName);
        }*/
    }
}
