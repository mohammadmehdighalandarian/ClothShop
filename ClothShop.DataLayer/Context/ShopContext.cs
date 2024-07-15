using ClothShop.DataLayer.Entities.Order;
using ClothShop.DataLayer.Entities.Permission;
using ClothShop.DataLayer.Entities.Product;
using ClothShop.DataLayer.Entities.Product.ProductDetails;
using ClothShop.DataLayer.Entities.User;
using ClothShop.DataLayer.Entities.Wallet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ClothShop.DataLayer.Context;

public class ShopContext:DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    {
        
    }

    #region Product

    public DbSet<Product> Products { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<UseType> UseTypes { get; set; }
    public DbSet<ProductComment> ProductComments { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<ProductMaterial> ProductMaterials { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }
    public DbSet<ProductUseType> ProductUseTypes { get; set; }
    public DbSet<UserProduct> UserProducts { get; set; }
    
    #endregion
    #region User

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserDiscountCode> UserDiscountCodes { get; set; }

    #endregion
    #region Order

    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    #endregion
    #region Wallet

    public DbSet<WalletType> WalletTypes { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    #endregion

    //#region Permission

    //public DbSet<Permision> Permission { get; set; }
    //public DbSet<RolePermision> RolePermission { get; set; }

    //#endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        //modelBuilder.ApplyConfiguration(new UserMapping());
        //modelBuilder.ApplyConfiguration(new UserRoleMapping());
        //modelBuilder.ApplyConfiguration(new RoleMapping());
        //modelBuilder.ApplyConfiguration(new WalletMapping());
        //modelBuilder.ApplyConfiguration(new WalletTypeMapping());
        //modelBuilder.Entity<User>().HasQueryFilter(u => u.Isdeleted == false);
        //modelBuilder.Entity<Role>().HasQueryFilter(x => x.IsDeleted == false);
        //modelBuilder.Entity<CourseGroup>().HasQueryFilter(x => x.IsDelete == false);

        //var assembly = typeof(UserMapping).Assembly;
        //modelBuilder.ApplyConfigurationsFromAssembly(assembly);



    }
}