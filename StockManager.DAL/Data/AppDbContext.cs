using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockManager.DAL.Entities;

namespace StockManager.DAL.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("dbo");

            // Identity tables config
            builder.Entity<IdentityUser>(opt =>
            {
                opt.ToTable(name: "Users", schema: "Identity");
                opt.Property(e => e.UserName).HasMaxLength(100);
                opt.Property(e => e.NormalizedUserName).HasMaxLength(100);
                opt.Property(e => e.Email).HasMaxLength(100);
                opt.Property(e => e.NormalizedEmail).HasMaxLength(100);
                opt.Property(e => e.PhoneNumber).HasMaxLength(20);
            });
            builder.Entity<IdentityRole>(opt =>
            {
                opt.ToTable(name: "Roles", schema: "Identity");
                opt.Property(e => e.Name).HasMaxLength(100);
                opt.Property(e => e.NormalizedName).HasMaxLength(100);
            });
            builder.Entity<IdentityRoleClaim<string>>(opt => { opt.ToTable(name: "RoleClaims", schema: "Identity"); });
            builder.Entity<IdentityUserClaim<string>>(opt => { opt.ToTable(name: "UserClaims", schema: "Identity"); });
            builder.Entity<IdentityUserLogin<string>>(opt => { opt.ToTable(name: "UserLogins", schema: "Identity"); });
            builder.Entity<IdentityUserRole<string>>(opt => { opt.ToTable(name: "UserRoles", schema: "Identity"); });
            builder.Entity<IdentityUserToken<string>>(opt => { opt.ToTable(name: "UserTokens", schema: "Identity"); });

            builder.Entity<Warehouse>()
            .Property(e => e.CreatedDateTime)
            .HasDefaultValueSql("GETDATE()")  // SQL function to set the default date as current date
            .ValueGeneratedOnAdd();  // Automatically generated when an entity is added

            builder.Entity<Warehouse>()
                .Property(e => e.UpdatedDateTime)
                .HasDefaultValueSql("GETDATE()")  // SQL function to set the default date
                .ValueGeneratedOnAddOrUpdate();  // Automatically updated when an entity is added or updated


            builder.Entity<Warehouse>()
                .HasOne(w => w.WarehouseManager)
                .WithOne()
                .HasForeignKey<Warehouse>(w => w.WarehouseManagerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);


            // Disable cascading delete for
            // Category -> Subcategory 
            builder.Entity<Category>()
                .HasMany(c => c.Subcategories)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);

            // Subcategory -> Product 
            builder.Entity<Subcategory>()
                .HasMany(e => e.Products)
                .WithOne(e => e.Subcategory)
                .HasForeignKey(e => e.SubcategoryId)
                .IsRequired(required: false)
                .OnDelete(DeleteBehavior.Restrict);

            // Warehouse -> Transaction 
            builder.Entity<Warehouse>()
                .HasMany(w => w.Transactions)
                .WithOne(w => w.Warehouse)
                .HasForeignKey(w => w.WarehouseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Warhouse -> Purchase
            builder.Entity<Warehouse>()
                .HasMany(w => w.Purchases)
                .WithOne(w => w.Warehouse)
                .HasForeignKey(w => w.WarehouseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Warhouse -> Sales
            builder.Entity<Warehouse>()
                .HasMany(w => w.Sales)
                .WithOne(w => w.Warehouse)
                .HasForeignKey(w => w.WarehouseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Transaction -> purchases
            builder.Entity<Transaction>()
                .HasMany(w => w.Purchases)
                .WithOne(w => w.Transaction)
                .HasForeignKey(w => w.TransactionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Transaction -> Sales
            builder.Entity<Transaction>()
                .HasMany(w => w.Sales)
                .WithOne(w => w.Transaction)
                .HasForeignKey(w => w.TransactionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Restrict Product table to be deleted
            builder.Entity<Product>()
                .HasOne(p => p.Subcategory)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SubcategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProductWarehouse>()
                .ToTable("ProductWarehouse");

            builder.Entity<ProductWarehouse>()
                .HasKey(pw => new { pw.ProductId, pw.WarehouseId });

            builder.Entity<ProductWarehouse>()
                .HasOne(pw => pw.Product)
                .WithMany(p => p.ProductWarehouses)
                .HasForeignKey(pw => pw.ProductId);

            builder.Entity<ProductWarehouse>()
                .HasOne(pw => pw.Warehouse)
                .WithMany(w => w.ProductWarehouses)
                .HasForeignKey(pw => pw.WarehouseId);

            builder.Entity<ProductWarehouse>()
                .Property(pw => pw.StockQuantity)
                .HasColumnName("StockQuantity");

            // seed roles to Db
            List<IdentityRole> roles =
            [
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "warehouse manager",
                    NormalizedName = "WAREHOUSE MANAGER"
                }
            ];

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
