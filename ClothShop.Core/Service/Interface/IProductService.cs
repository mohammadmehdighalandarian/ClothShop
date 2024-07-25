using ClothShop.Core.DTOs.Product;
using ClothShop.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClothShop.Core.Service.Interface;

public interface IProductService
{
    #region Group

    List<ProductGroup> GetAllGroup();
    List<SelectListItem> GetGroupForManageProduct();
    List<SelectListItem> GetSubGroupForManageProduct(int groupId);
    List<SelectListItem> GetMaterials();
    List<SelectListItem> GetSizes();
    List<SelectListItem> GetUseTypes();
    ProductGroup GetById(int groupId);
    void AddGroup(ProductGroup group);
    void UpdateGroup(ProductGroup group);

    #endregion

    #region Course

    List<ShowProductForAdmin> GetProductsForAdmin();

    int AddProduct(Product product, IFormFile imgProduct);
    Product GetProductById(int productId);
    void UpdateProduct(Product product, IFormFile imgProduct, IFormFile productDemo);

    Tuple<List<ShowProductForAdmin>, int> GetProduct(int pageId = 1, string filter = "", string getType = "all",
        string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);

    Product GetProductForShow(int productId);

    List<ShowProductForAdmin> GetPopularProduct();
    bool IsFree(int productId);

    #endregion
}