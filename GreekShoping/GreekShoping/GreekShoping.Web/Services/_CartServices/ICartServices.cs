using GreekShoping.Web.Models;

namespace GreekShoping.Web.Services._CartServices;

public interface ICartServices
{
    Task<CartViewModel> FindCartByUserId(string iserId, string token);
    Task<CartViewModel> AddItemToCart(CartViewModel cart, string token);
    Task<CartViewModel> UpdateCart(CartViewModel cart, string token);
    Task<bool> RemoveFromCart(long cartId, string token);

    Task<bool> ApplyCoupon(CartViewModel cart, string couponCode, string token);
    Task<bool> RemoveCoupon(string iserId, string token);
    Task<bool> ClearCart(string iserId, string token);
    Task<CartViewModel> CheckOut(CartHeaderViewModel cartHeader, string token);

}
