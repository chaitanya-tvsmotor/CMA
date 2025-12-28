using CMA.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMA.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Salary> Salaries { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure Category
        builder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            
            entity.HasOne(e => e.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(e => e.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure Brand
        builder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure Product
        builder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).HasPrecision(18, 2);
            entity.Property(e => e.Length).HasPrecision(10, 2);
            entity.Property(e => e.Width).HasPrecision(10, 2);
            entity.Property(e => e.Height).HasPrecision(10, 2);
            entity.Property(e => e.Weight).HasPrecision(10, 2);
            
            entity.HasOne(e => e.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(e => e.CategoryId);
            
            entity.HasOne(e => e.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(e => e.BrandId);
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure Employee
        builder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EmployeeId).IsRequired().HasMaxLength(50);
            entity.Property(e => e.BasicSalary).HasPrecision(18, 2);
            
            entity.HasOne(e => e.User)
                .WithOne(u => u.EmployeeProfile)
                .HasForeignKey<Employee>(e => e.UserId);
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure Attendance
        builder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.WorkHours).HasPrecision(5, 2);
            entity.Property(e => e.OvertimeHours).HasPrecision(5, 2);
            
            entity.HasOne(e => e.Employee)
                .WithMany(emp => emp.Attendances)
                .HasForeignKey(e => e.EmployeeId);
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure Salary
        builder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.BasicSalary).HasPrecision(18, 2);
            entity.Property(e => e.Allowances).HasPrecision(18, 2);
            entity.Property(e => e.Bonus).HasPrecision(18, 2);
            entity.Property(e => e.Deductions).HasPrecision(18, 2);
            entity.Property(e => e.NetSalary).HasPrecision(18, 2);
            entity.Property(e => e.HousingAllowance).HasPrecision(18, 2);
            entity.Property(e => e.TransportAllowance).HasPrecision(18, 2);
            entity.Property(e => e.MedicalAllowance).HasPrecision(18, 2);
            entity.Property(e => e.TaxDeduction).HasPrecision(18, 2);
            entity.Property(e => e.LoanDeduction).HasPrecision(18, 2);
            entity.Property(e => e.OvertimePay).HasPrecision(18, 2);
            
            entity.HasOne(e => e.Employee)
                .WithMany(emp => emp.Salaries)
                .HasForeignKey(e => e.EmployeeId);
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure Order
        builder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
            
            entity.HasOne(e => e.Dealer)
                .WithMany(u => u.DealerOrders)
                .HasForeignKey(e => e.DealerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.SalesAgent)
                .WithMany(u => u.SalesAgentOrders)
                .HasForeignKey(e => e.SalesAgentId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Manager)
                .WithMany(u => u.ManagerOrders)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure OrderItem
        builder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
            entity.Property(e => e.TotalPrice).HasPrecision(18, 2);
            
            entity.HasOne(e => e.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(e => e.OrderId);
            
            entity.HasOne(e => e.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(e => e.ProductId);
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure Vehicle
        builder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.VehicleNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LicensePlate).IsRequired().HasMaxLength(20);
            
            entity.HasOne(e => e.AssignedTo)
                .WithMany(u => u.AssignedVehicles)
                .HasForeignKey(e => e.AssignedToId)
                .OnDelete(DeleteBehavior.SetNull);
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure PurchaseOrder
        builder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PONumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
            
            entity.HasOne(e => e.Supplier)
                .WithMany(s => s.PurchaseOrders)
                .HasForeignKey(e => e.SupplierId);
            
            entity.HasOne(e => e.Manager)
                .WithMany(u => u.PurchaseOrders)
                .HasForeignKey(e => e.ManagerId);
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure PurchaseOrderItem
        builder.Entity<PurchaseOrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
            entity.Property(e => e.TotalPrice).HasPrecision(18, 2);
            
            entity.HasOne(e => e.PurchaseOrder)
                .WithMany(po => po.Items)
                .HasForeignKey(e => e.PurchaseOrderId);
            
            entity.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId);
            
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Configure Supplier
        builder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.HasQueryFilter(e => !e.IsDeleted);
        });
    }
}
