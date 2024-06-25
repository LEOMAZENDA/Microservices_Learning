using GreekShoping.Web.Models;

namespace GreekShoping.Web.Services._CartServices;

public interface ICartService
{
    Task<CartViewModel> FindCartByUserId(string userId, string token);
    Task<CartViewModel> AddItemToCart(CartViewModel cart, string token);
    Task<CartViewModel> UpdateCart(CartViewModel cart, string token);
    Task<bool> RemoveFromCart(long cartId, string token);
    Task<bool> ClearCart(string userId, string token);

    Task<bool> ApplyCoupon(CartViewModel model, string token);
    Task<bool> RemoveCoupon(string userId, string token);

    Task<CartViewModel> CheckOut(CartHeaderViewModel cartHeader, string token);

}
