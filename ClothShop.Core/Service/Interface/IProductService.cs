using ClothShop.Core.DTOs.Product;
using ClothShop.DataLayer.Entities.Product;
using ClothShop.DataLayer.Entities.Product.ProductDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Drawing2D;

namespace ClothShop.Core.Service.Interface;

public interface IProductService
{
    List<Material> GetMaterials();

    #region Color

    List<Color> GetAllColor();
    Color GetColor(int id);
    void AddColor(Color color);
    void UpdateColor(Color color);

    #endregion

    #region size

    List<Size> GetAllSizes();
    Size GetSize(int id);
    void Addsize(Size size);
    void Updatesize(Size size);

    #endregion

    #region Matrial

    List<Material> GetAllMatrial();
    Material GetMaterial(int id);
    void AddMatrial(Material matrial);
    void UpdateMatrial(Material matrial);

    #endregion

    #region Group

    List<ProductGroup> GetAllGroup();
    List<SelectListItem> GetGroupForManageProduct();
    List<SelectListItem> GetSubGroupForManageProduct(int groupId);

    List<UseType> GetUseTypes();
    ProductGroup GetById(int groupId);
    void AddGroup(ProductGroup group);
    void UpdateGroup(ProductGroup group);

    #endregion

    #region Product

    List<ShowProductForAdmin> GetProductsForAdmin();
    void AddProductImage(int productId, IFormFile Images);
    int AddProduct(Product product, IFormFile imgProduct);
    Image[] GetAllImagesOfProduct(int productId);
    void AddSizeToProductSize(int productId, List<SizeWithCountForAdmin> Sizes);
    void AddMaterialToProductMaterial(int productId, List<int> materials);
    void AddusetypeToProductusetype(int productId, List<int> usetypes);
    Product GetProductById(int productId);
    void DeActiveProduct(Product product);
    public List<SelectListItem> GetMaterailOfProduct(int productId);
    void ActiveProduct(int ProductId);
    void UpdateProduct(Product product, IFormFile imgProduct, IFormFile productDemo);
    List<SelectListItem> GetSizesOfProduct(int productId);
    int GetQuantityOfSize(int productSizeId);
    Tuple<List<ShowProductListItemViewModel>, int> GetProduct(int pageId = 1, string filter = "", string getType = "all",
        string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);
    Product GetProductForShow(int productId);
    List<ShowProductListItemViewModel> GetPopularProduct();
    List<int> GetGroupsOfProduct(List<ShowProductListItemViewModel> products);

    #endregion

    #region Comments

    void AddComment(ProductComment comment);
    Tuple<List<ProductComment>, int> GetProductComment(int ProductId, int pageId = 1);

    #endregion
}