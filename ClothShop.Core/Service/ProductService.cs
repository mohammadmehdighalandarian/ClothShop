using System.Runtime.CompilerServices;
using ClothShop.Core.Convertors;
using ClothShop.Core.DTOs.Product;
using ClothShop.Core.Generator;
using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using ClothShop.DataLayer.Context;
using ClothShop.DataLayer.Entities.Product;
using ClothShop.DataLayer.Entities.Product.ProductDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClothShop.Core.Service;

public class ProductService : IProductService
{
    private ShopContext _context;

    public ProductService(ShopContext context)
    {
        _context = context;
    }

    public void UpdateMatrial(Material matrial)
    {
        _context.Materials.Update(matrial);
        _context.SaveChanges();
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
    public List<Material> GetMaterials()
    {
        return _context.Materials.ToList();
    }
    public List<Color> GetAllColor()
    {
        return _context.Colors.ToList();
    }

    public Color GetColor(int id)
    {
        return _context.Colors.Find(id);
    }

    public void AddColor(Color color)
    {
        _context.Colors.Add(color);
        _context.SaveChanges();
    }
    public void UpdateColor(Color color)
    {
        _context.Colors.Update(color);
        _context.SaveChanges();
    }
    public List<Size> GetAllSizes()
    {
        return _context.Sizes.ToList();
    }

    public Size GetSize(int id)
    {
        return _context.Sizes.Find(id);
    }

    public void Addsize(Size size)
    {
        _context.Sizes.Add(size);
        _context.SaveChanges();
    }
    public void Updatesize(Size size)
    {
        _context.Update(size);
        _context.SaveChanges();
    }
    public List<Material> GetAllMatrial()
    {
        return _context.Materials.ToList();
    }

    public Material GetMaterial(int id)
    {
        return _context.Materials.Find(id);
    }

    public void AddMatrial(Material matrial)
    {
        _context.Materials.Add(matrial);
        _context.SaveChanges();
    }
    public List<UseType> GetUseTypes()
    {
        return _context.UseTypes.ToList();
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

    public void AddProductImage(int productId, IFormFile Image)
    {



        Image newImage = new Image();
        newImage.ProductId = productId;
        if (Image is not null && Image.IsImage())
        {
            newImage.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(Image.FileName);
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Product/image", newImage.ImageName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                Image.CopyTo(stream);
            }

            ImageConvertor imgResizer = new ImageConvertor();
            string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Product/thumb", newImage.ImageName);

            imgResizer.Image_resize(imagePath, thumbPath, 250);
        }

        _context.Images.Add(newImage);


        _context.SaveChanges();
    }

    public int AddProduct(Product product, IFormFile imgProduct)
    {

        product.CreationDate = DateTime.Now;
        product.ProductImageName = "no-photo.jpg";
        //TODO Check Image

        if (imgProduct is not null && imgProduct.IsImage())
        {
            product.ProductImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgProduct.FileName);
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Product/image", product.ProductImageName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                imgProduct.CopyTo(stream);
            }

            ImageConvertor imgResizer = new ImageConvertor();
            string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Product/thumb", product.ProductImageName);

            imgResizer.Image_resize(imagePath, thumbPath, 250);
        }

        _context.Add(product);
        _context.SaveChanges();

        return product.ProductId;
    }

    public Image[] GetAllImagesOfProduct(int productId)
    {
        return _context.Images.Where(x => x.ProductId == productId).ToArray();
    }

    public void AddSizeToProductSize(int productId, List<SizeWithCountForAdmin> Sizes)
    {
        foreach (var sizeid in Sizes)
        {
            if (sizeid.Quantity != 0)
            {
                _context.ProductSizes.Add(new ProductSize()
                {
                    ProductId = productId,
                    SizeId = sizeid.SizeId,
                    Count = sizeid.Quantity
                });
            }

        }

        _context.SaveChanges();
    }
    public void AddMaterialToProductMaterial(int productId, List<int> materials)
    {
        foreach (var materialid in materials)
        {
            _context.ProductMaterials.Add(new ProductMaterial()
            {
                ProductId = productId,
                MaterialId = materialid
            });
        }

        _context.SaveChanges();
    }
    public void AddusetypeToProductusetype(int productId, List<int> usetypes)
    {
        foreach (var usetypeId in usetypes)
        {
            _context.ProductUseTypes.Add(new ProductUseType()
            {
                ProductId = productId,
                UseTypeId = usetypeId
            });
        }

        _context.SaveChanges();
    }
    public Product GetProductById(int productId)
    {
        return _context.Products.Find(productId);
    }
    public void DeActiveProduct(Product product)
    {
        var Product = GetProductById(product.ProductId);
        Product.IsActive = false;
        _context.Update(Product);
        _context.SaveChanges();
    }
    public void ActiveProduct(int ProductId)
    {
        var Product = GetProductById(ProductId);
        Product.IsActive = true;
        _context.Update(Product);
        _context.SaveChanges();
    }
    public void UpdateProduct(Product product, IFormFile imgProduct, IFormFile productDemo)
    {
        throw new NotImplementedException();
    }
    public List<SelectListItem> GetSizesOfProduct(int productId)
    {
        return _context.ProductSizes
            .Where(g => g.ProductId == productId)
            .Select(g => new SelectListItem()
            {
                Text = g.Size.SizeNO,
                Value = g.ProductSizeId.ToString(),
                Disabled = g.Count != 0
            }).ToList();
    }

    public List<SelectListItem> GetMaterailOfProduct(int productId)
    {
        return _context.ProductMaterials
            .Where(g => g.ProductId == productId)
            .Select(g => new SelectListItem()
            {
                Text = g.Material.MaterialName,
                Value = g.Material.MaterialId.ToString()
            }).ToList();
    }

    public int GetQuantityOfSize(int productSizeId)
    {
        return _context.ProductSizes
            .Where(x => x.ProductSizeId == productSizeId)
            .Select(x => x.Count)
            .FirstOrDefault(); ;
    }
    public Tuple<List<ShowProductListItemViewModel>, int> GetProduct(int pageId = 1,
        string filter = "", string getType = "all", string orderByType = "date",
        int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
    {
        if (take == 0)
            take = 8;

        IQueryable<Product> result = _context.Products;
        result = result.Where(c => c.IsActive==true);

        if (!string.IsNullOrEmpty(filter))
        {
            result = result.Where(c => c.ProductTitle.Contains(filter) || c.Tags.Contains(filter));
        }

        switch (getType)
        {
            case "all":
                break;
            case "buy":
                {
                    result = result.Where(c => c.ProductPrice != 0);
                    break;
                }
            case "free":
                {
                    result = result.Where(c => c.ProductPrice == 0);
                    break;
                }

        }

        switch (orderByType)
        {
            case "date":
                {
                    result = result.OrderByDescending(c => c.CreationDate);
                    break;
                }
            case "updatedate":
                {
                    result = result.OrderByDescending(c => c.UpdateDate);
                    break;
                }
        }

        if (startPrice > 0)
        {
            result = result.Where(c => c.ProductPrice > startPrice);
        }

        if (endPrice > 0)
        {
            result = result.Where(c => c.ProductPrice < startPrice);
        }


        if (selectedGroups != null && selectedGroups.Any())
        {
            foreach (int groupId in selectedGroups)
            {
                result = result.Where(c => c.GroupId == groupId || c.SubGroupId == groupId);
            }

        }

        int skip = (pageId - 1) * take;

        int pageCount = result.Select(c => new ShowProductListItemViewModel()
        {
            ProductId = c.ProductId,
            ImageName = c.ProductImageName,
            Price = c.ProductPrice,
            Title = c.ProductTitle,
            GroupId = c.GroupId,
            subGroupId = c.SubGroupId.GetValueOrDefault() // Returns 0 if nullableInt is null
        }).Count() / take;

        var query = result.Select(c => new ShowProductListItemViewModel()
        {
            ProductId = c.ProductId,
            ImageName = c.ProductImageName,
            Price = c.ProductPrice,
            Title = c.ProductTitle,
            GroupId = c.GroupId,
            subGroupId = c.SubGroupId.GetValueOrDefault() // Returns 0 if nullableInt is null

        }).Skip(skip).Take(take).ToList();

        return Tuple.Create(query, pageCount);
    }
    public Product GetProductForShow(int productId)
    {
        return _context.Products.Find(productId);
    }
    public List<ShowProductListItemViewModel> GetPopularProduct()
    {
        var result= _context.Products
            .Where(x=>x.IsActive==true)
            .Include(c => c.OrderDetails)
            .Where(c => c.OrderDetails.Any())
            .OrderByDescending(d => d.OrderDetails.Count)
            .Take(8)
            .Select(c => new ShowProductListItemViewModel()
            {
                ProductId = c.ProductId,
                ImageName = c.ProductImageName,
                Price = c.ProductPrice,
                Title = c.ProductTitle,
                GroupId = c.GroupId,
                subGroupId = c.SubGroupId.GetValueOrDefault()
            })
            .ToList();

        return result;
    }
    public List<int> GetGroupsOfProduct(List<ShowProductListItemViewModel> products)
    {
        List<int> Groups = new List<int>();
        foreach (var product in products)
        {
            Groups.Add(product.GroupId);
            if (product.subGroupId is not 0)
                Groups.Add(product.subGroupId);
        }
        return Groups;
    }
    public void AddComment(ProductComment comment)
    {
        _context.ProductComments.Add(comment);
        _context.SaveChanges();
    }
    public Tuple<List<ProductComment>, int> GetProductComment(int ProductId, int pageId = 1)
    {
        int take = 5;
        int skip = (pageId - 1) * take;
        int pageCount = _context.ProductComments.Where(c => !c.IsDelete && c.ProductId == ProductId).Count() / take;

        if ((pageCount % 2) != 0)
        {
            pageCount += 1;
        }

        return Tuple.Create(
            _context.ProductComments.Include(c => c.User).Where(c => !c.IsDelete && c.ProductId == ProductId).Skip(skip).Take(take)
                .OrderByDescending(c => c.CreateDate).ToList(), pageCount);
    }
}