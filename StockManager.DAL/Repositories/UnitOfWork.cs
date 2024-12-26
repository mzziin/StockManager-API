using Microsoft.EntityFrameworkCore.Storage;
using StockManager.DAL.Data;
using StockManager.DAL.Entities;
using StockManager.DAL.Repositories.ProductWarehouseRepositories;
using StockManager.DAL.Repositories.TransactionRepositories;

namespace StockManager.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed = false;

        public IGenericRepository<Product> Products { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IGenericRepository<Customer> Customers { get; private set; }
        public IGenericRepository<Purchase> Purchases { get; private set; }
        public IGenericRepository<Sale> Sales { get; private set; }
        public IGenericRepository<Subcategory> Subcategories { get; private set; }
        public IGenericRepository<Supplier> Suppliers { get; private set; }
        public IGenericRepository<Warehouse> Warehouses { get; private set; }
        public IGenericRepository<ProductSale> ProductSales { get; private set; }
        public IGenericRepository<ProductPurchase> ProductPurchases { get; private set; }
        public ITransactionRepository Transactions { get; private set; }
        public IProductWarehouseRepository ProductWarehouses { get; private set; }

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
            Products = new GenericRepository<Product>(_context);
            Categories = new GenericRepository<Category>(_context);
            Customers = new GenericRepository<Customer>(_context);
            Purchases = new GenericRepository<Purchase>(_context);
            Sales = new GenericRepository<Sale>(_context);
            Subcategories = new GenericRepository<Subcategory>(_context);
            Suppliers = new GenericRepository<Supplier>(_context);
            Warehouses = new GenericRepository<Warehouse>(_context);
            ProductSales = new GenericRepository<ProductSale>(_context);
            ProductPurchases = new GenericRepository<ProductPurchase>(_context);
            Transactions = new TransactionRepository(_context);
            ProductWarehouses = new ProductWarehouseRepository(_context);
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }
            _transaction = await _context.Database.BeginTransactionAsync();
        }


        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await RollBack();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null!;
                }
            }
        }

        public async Task RollBack()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                    await _transaction.DisposeAsync();
                    _transaction = null!;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.DisposeAsync();
                    _context?.DisposeAsync();
                }
                _disposed = true;
            }
        }
    }
}