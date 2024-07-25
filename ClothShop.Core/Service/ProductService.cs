using ClothShop.Core.Convertors;
using ClothShop.Core.DTOs.Product;
using ClothShop.Core.Generator;
using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using ClothShop.DataLayer.Context;
using ClothShop.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClothShop.Core.Service;

public class ProductService : IProductService
{
    private ShopContext _context;

    public ProductService(ShopContext context)
    {
        _context = context;
    }

    public List<ProductGroup> GetAllGroup()
    {
        return _context.ProductGroups
            .Include(c => c.ProductGroups)
            .ToList();
    }

    public List<SelectListItem> GetGroupForManageProduct()
    {
        return _context.ProductGroups
            .Where(g => g.ParentId == null)
            .Select(g => new SelectListItem()
            {
                Text = g.GroupTitle,
                Value = g.GroupId.ToString()
            }).ToList();
    }

    public List<SelectListItem> GetSubGroupForManageProduct(int groupId)
    {
        return _context.ProductGroups
            .Where(g => g.ParentId == groupId)
            .Select(g => new SelectListItem()
            {
                Text = g.GroupTitle,
                Value = g.GroupId.ToString()
            }).ToList();
    }

    public List<SelectListItem> GetMaterials()
    {
        return _context.Materials
            .Select(u => new SelectListItem()
            {
                Value = u.MaterialId.ToString(),
                Text = u.MaterialName
            }).ToList();
    }
    public List<SelectListItem> GetSizes()
    {
        return _context.Sizes
            .Select(u => new SelectListItem()
            {
                Value = u.SizeId.ToString(),
                Text = u.SizeNO
            }).ToList();
    }

    public List<SelectListItem> GetUseTypes()
    {
        return _context.UseTypes
            .Select(u => new SelectListItem()
            {
                Value = u.TypeId.ToString(),
                Text = u.TypeName
            }).ToList();
    }



    public ProductGroup GetById(int groupId)
    {
        return _context.ProductGroups.Find(groupId);
    }

    public void AddGroup(ProductGroup @group)
    {
        _context.ProductGroups.Add(group);
        _context.SaveChanges();
    }

    public void UpdateGroup(ProductGroup @group)
    {
        _context.ProductGroups.Update(group);
        _context.SaveChanges();
    }

    public List<ShowProductForAdmin> GetProductsForAdmin()
    {
        return _context.Products
            .Include(p => p.ProductSubGroup) // Include the related Group entity
            .Include(p => p.ProductGroup) // Include the related SubGroup entity
            .Select(x => new ShowProductForAdmin()
            {
                ProductId = x.ProductId,
                ProductTitle = x.ProductTitle,
                ProductImageName = x.ProductImageName,
                SubGroup = x.ProductSubGroup.GroupTitle,  // Get SubGroup name
                MainGroup = x.ProductGroup.GroupTitle, // Get Group name
                IsActive = x.IsActive
            }).ToList();
    }

    public int AddProduct(Product product, IFormFile imgProduct)
    {
        product.CreationDate = DateTime.Now;
        product.ProductImageName = "no-photo.jpg";
        //TODO Check Image
        if (imgProduct != null && imgProduct.IsImage())
        {
            product.ProductImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgProduct.FileName);
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", product.ProductImageName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                imgProduct.CopyTo(stream);
            }

            ImageConvertor imgResizer = new ImageConvertor();
            string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", product.ProductImageName);

            imgResizer.Image_resize(imagePath, thumbPath, 250);
        }

        _context.Add(product);
        _context.SaveChanges();

        return product.ProductId;
    }

    public Product GetProductById(int productId)
    {
        throw new NotImplementedException();
    }

    public void UpdateProduct(Product product, IFormFile imgProduct, IFormFile productDemo)
    {
        throw new NotImplementedException();
    }

    public Tuple<List<ShowProductForAdmin>, int> GetProduct(int pageId = 1, string filter = "", string getType = "all", string orderByType = "date",
        int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
    {
        throw new NotImplementedException();
    }

    public Product GetProductForShow(int productId)
    {
        throw new NotImplementedException();
    }

    public List<ShowProductForAdmin> GetPopularProduct()
    {
        throw new NotImplementedException();
    }

    public bool IsFree(int productId)
    {
        throw new NotImplementedException();
    }
}