using CMA.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CMA.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Seed Roles
        string[] roles = { "Supervisor", "SalesAgent", "Dealer", "Manager", "Driver", "Accountant", "Worker" };
        
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Seed Supervisor
        if (await userManager.FindByEmailAsync("supervisor@cma.com") == null)
        {
            var supervisor = new ApplicationUser
            {
                UserName = "supervisor",
                Email = "supervisor@cma.com",
                FirstName = "John",
                LastName = "Supervisor",
                EmailConfirmed = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            await userManager.CreateAsync(supervisor, "Supervisor@123");
            await userManager.AddToRoleAsync(supervisor, "Supervisor");
        }

        // Seed Manager
        if (await userManager.FindByEmailAsync("manager@cma.com") == null)
        {
            var manager = new ApplicationUser
            {
                UserName = "manager",
                Email = "manager@cma.com",
                FirstName = "Jane",
                LastName = "Manager",
                EmailConfirmed = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            await userManager.CreateAsync(manager, "Manager@123");
            await userManager.AddToRoleAsync(manager, "Manager");
        }

        // Seed Categories
        if (!context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new Category { Name = "Motorcycles", Description = "All types of motorcycles", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Category { Name = "Accessories", Description = "Motorcycle accessories and gear", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Category { Name = "Parts", Description = "Spare parts and components", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Category { Name = "Safety Equipment", Description = "Safety gear and equipment", IsActive = true, CreatedAt = DateTime.UtcNow }
            };
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        // Seed Brands
        if (!context.Brands.Any())
        {
            var brands = new List<Brand>
            {
                new Brand { Name = "Honda", Description = "Honda Motor Company", Country = "Japan", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Brand { Name = "Yamaha", Description = "Yamaha Motor Company", Country = "Japan", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Brand { Name = "TVS", Description = "TVS Motor Company", Country = "India", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Brand { Name = "Hero", Description = "Hero MotoCorp", Country = "India", IsActive = true, CreatedAt = DateTime.UtcNow }
            };
            context.Brands.AddRange(brands);
            await context.SaveChangesAsync();
        }

        // Seed Products
        if (!context.Products.Any())
        {
            var motorcycleCategory = context.Categories.First(c => c.Name == "Motorcycles");
            var accessoryCategory = context.Categories.First(c => c.Name == "Accessories");
            var hondaBrand = context.Brands.First(b => b.Name == "Honda");
            var yamahaBrand = context.Brands.First(b => b.Name == "Yamaha");
            
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Honda CB500X",
                    Description = "Adventure motorcycle with advanced features",
                    CategoryId = motorcycleCategory.Id,
                    BrandId = hondaBrand.Id,
                    Price = 15000.00m,
                    StockQuantity = 25,
                    ImageUrl = "/images/honda-cb500x.jpg",
                    Specifications = "Engine: 500cc, Power: 47HP, Top Speed: 180km/h",
                    Length = 215,
                    Width = 83,
                    Height = 140,
                    Weight = 196,
                    SKU = "HON-CB500X-2024",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Yamaha MT-07",
                    Description = "Lightweight sport bike for urban riding",
                    CategoryId = motorcycleCategory.Id,
                    BrandId = yamahaBrand.Id,
                    Price = 12000.00m,
                    StockQuantity = 30,
                    ImageUrl = "/images/yamaha-mt07.jpg",
                    Specifications = "Engine: 689cc, Power: 73HP, Top Speed: 210km/h",
                    Length = 208,
                    Width = 80,
                    Height = 110,
                    Weight = 184,
                    SKU = "YAM-MT07-2024",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Premium Helmet",
                    Description = "Safety helmet with advanced protection",
                    CategoryId = accessoryCategory.Id,
                    BrandId = hondaBrand.Id,
                    Price = 150.00m,
                    StockQuantity = 100,
                    ImageUrl = "/images/helmet.jpg",
                    Specifications = "DOT certified, Multiple sizes available",
                    Length = 30,
                    Width = 25,
                    Height = 25,
                    Weight = 1.5m,
                    SKU = "ACC-HELM-PRE-001",
                    CreatedAt = DateTime.UtcNow
                }
            };
            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }

        // Seed Suppliers
        if (!context.Suppliers.Any())
        {
            var suppliers = new List<Supplier>
            {
                new Supplier
                {
                    Name = "Global Parts Supplier",
                    ContactPerson = "Robert Smith",
                    Email = "contact@globalparts.com",
                    Phone = "+1-555-0123",
                    Address = "123 Industrial Blvd",
                    City = "Detroit",
                    State = "MI",
                    PostalCode = "48201",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Supplier
                {
                    Name = "Motorcycle Components Inc",
                    ContactPerson = "Lisa Johnson",
                    Email = "info@motorcyclecomp.com",
                    Phone = "+1-555-0456",
                    Address = "456 Factory Road",
                    City = "Milwaukee",
                    State = "WI",
                    PostalCode = "53202",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };
            context.Suppliers.AddRange(suppliers);
            await context.SaveChangesAsync();
        }
    }
}
