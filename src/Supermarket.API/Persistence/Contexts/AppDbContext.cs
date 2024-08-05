using Microsoft.EntityFrameworkCore;
using Supermarket.Domain.Models;

namespace Supermarket.API.Persistence.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Supermarket.Domain.Models.Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Supermarket.Domain.Models.Category>().ToTable("Categories");
        builder.Entity<Supermarket.Domain.Models.Category>().HasKey(p => p.Id);
        builder.Entity<Supermarket.Domain.Models.Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();//.HasValueGenerator<InMemoryIntegerValueGenerator<int>>();
        builder.Entity<Supermarket.Domain.Models.Category>().Property(p => p.Name).IsRequired().HasMaxLength(30);
        builder.Entity<Supermarket.Domain.Models.Category>().HasMany(p => p.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);

        builder.Entity<Supermarket.Domain.Models.Category>().HasData
        (
            new Supermarket.Domain.Models.Category { Id = 100, Name = "Fruits and Vegetables" }, // Id set manually due to in-memory provider
            new Supermarket.Domain.Models.Category { Id = 101, Name = "Dairy" }
        );

        builder.Entity<Product>().ToTable("Products");
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Product>().Property(p => p.QuantityInPackage).IsRequired();
        builder.Entity<Product>().Property(p => p.UnitOfMeasurement).IsRequired();

        builder.Entity<Product>().HasData
        (
            new Product
            {
                Id = 100,
                Name = "Apple",
                QuantityInPackage = 1,
                UnitOfMeasurement = EUnitOfMeasurement.Unity,
                CategoryId = 100
            },
            new Product
            {
                Id = 101,
                Name = "Milk",
                QuantityInPackage = 2,
                UnitOfMeasurement = EUnitOfMeasurement.Liter,
                CategoryId = 101,
            }
        );
    }
}