using CMA.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CMA.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Seed Roles
        string[] roles = { "Supervisor", "SalesAgent", "Dealer", "Manager" };
        
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

        // Seed Products
        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Premium Motorcycle",
                    Description = "High-performance motorcycle with advanced features",
                    Category = "Motorcycles",
                    Price = 15000.00m,
                    StockQuantity = 50,
                    ImageUrl = "/images/motorcycle1.jpg",
                    Specifications = "Engine: 500cc, Power: 47HP, Top Speed: 180km/h",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Sport Bike",
                    Description = "Lightweight sport bike for urban riding",
                    Category = "Motorcycles",
                    Price = 12000.00m,
                    StockQuantity = 75,
                    ImageUrl = "/images/sportbike.jpg",
                    Specifications = "Engine: 400cc, Power: 40HP, Top Speed: 160km/h",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Cruiser Motorcycle",
                    Description = "Comfortable cruiser for long-distance rides",
                    Category = "Motorcycles",
                    Price = 18000.00m,
                    StockQuantity = 30,
                    ImageUrl = "/images/cruiser.jpg",
                    Specifications = "Engine: 650cc, Power: 55HP, Top Speed: 170km/h",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Motorcycle Helmet",
                    Description = "Safety helmet with advanced protection",
                    Category = "Accessories",
                    Price = 150.00m,
                    StockQuantity = 200,
                    ImageUrl = "/images/helmet.jpg",
                    Specifications = "DOT certified, Multiple sizes available",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Riding Jacket",
                    Description = "Protective riding jacket with armor",
                    Category = "Accessories",
                    Price = 250.00m,
                    StockQuantity = 150,
                    ImageUrl = "/images/jacket.jpg",
                    Specifications = "Waterproof, CE certified armor",
                    CreatedAt = DateTime.UtcNow
                }
            };
            context.Products.AddRange(products);
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
        }

        await context.SaveChangesAsync();
    }
}
