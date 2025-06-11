using Data.Configurations;
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class Web_Context : IdentityDbContext<User,Role,Guid>
    {
        public Web_Context(DbContextOptions<Web_Context> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new Categories_MaterialConfiguration());
            modelBuilder.ApplyConfiguration(new Categories_ProductConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeSchedulesConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeesConfiguration());
            modelBuilder.ApplyConfiguration(new ExportDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new ExportReceiptsConfiguration());
            modelBuilder.ApplyConfiguration(new ImportDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new ImportReceiptsConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryLogsConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialsConfiguration());
            modelBuilder.ApplyConfiguration(new LotConfiguration());
            modelBuilder.ApplyConfiguration(new LotDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new OrdersConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentsConfiguration());
            modelBuilder.ApplyConfiguration(new PositionsConfiguration());
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSizesConfiguration());
            modelBuilder.ApplyConfiguration(new RecipesConfiguration());
            modelBuilder.ApplyConfiguration(new SalariesConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftsConfiguration());
            modelBuilder.ApplyConfiguration(new SuppliersConfiguration());
            modelBuilder.ApplyConfiguration(new TablesConfiguration());
            modelBuilder.ApplyConfiguration(new TimekeepingConfiguration());
            modelBuilder.ApplyConfiguration(new ToppingsConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new OrderToppingDetailsConfiguration());

        }
        public DbSet<Categories_Material> Categories_Materials { get; set; }
        public DbSet<Categories_Products> Categories_Products { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<EmployeeSchedules> EmployeeSchedules { get; set; }
        public DbSet<ExportDetails> ExportDetails { get; set; }
        public DbSet<ExportReceipts> ExportReceipts { get; set; }
        public DbSet<ImportDetails> ImportDetails { get; set; }
        public DbSet<ImportReceipts> ImportReceipts { get; set; }
        public DbSet<InventoryLogs> InventoryLogs { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<LotDetails> LotsDetails { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductSizes> ProductSizes { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Salaries> Salaries { get; set; }
        public DbSet<Shifts> Shifts { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Tables> Tables { get; set; }
        public DbSet<Timekeeping> Timekeepings { get; set; }
        public DbSet<Toppings> Toppings { get; set; }
        public DbSet<OrderToppingDetails> OrderToppingsDetails { get; set; }

    }
}
