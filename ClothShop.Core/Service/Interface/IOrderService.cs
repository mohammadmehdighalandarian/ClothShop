using ClothShop.Core.DTOs.Order;
using ClothShop.DataLayer.Entities.Order;

namespace ClothShop.Core.Service.Interface
{
   public interface IOrderService
   {
       int AddOrder(string userName, int ProductId);

       void UpdatePriceOrder(int orderId);

       Order GetOrderForUserPanel(string userName, int orderId);
       Order GetOrderById(int orderId);

       bool FinalyOrder(string userName,int orderId);

       List<Order> GetUserOrders(string userName);

       void UpdateOrder(Order order);

     

       #region DisCount

       DiscountUseType UseDiscount(int orderId, string code);

       void AddDiscount(Discount discount);

       List<Discount> GetAllDiscounts();

       Discount GetDiscountById(int discountId);

       void UpdateDiscount(Discount discount);

       bool IsExistCode(string code);

       #endregion
   }
}
