using GreekShoping.Web.Models;
using GreekShoping.Web.Services._CartServices;
using GreekShoping.Web.Services._ProductServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreekShoping.Web.Controllers;

public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly ICartServices _cartServices;

    public CartController(
        IProductService productService, 
        ICartServices cartServices)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _cartServices = cartServices ?? throw new ArgumentNullException(nameof(cartServices));
    }

    [Authorize]
    public async Task<IActionResult> CartIndex()
    {
        
        return View(await FindUserCart());
    }

    [HttpPost]
    [ActionName("ApplyCoupon")]
    public async Task<IActionResult> ApplyCoupon(CartViewModel model)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

        var status = await _cartServices.ApplyCoupon(model, token);
        if (status)
            return RedirectToAction(nameof(CartIndex));
        return View();
    }


    [HttpPost]
    [ActionName("RemoveCoupon")]
    public async Task<IActionResult> RemoveCoupon(CartViewModel model)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

        var status = await _cartServices.RemoveCoupon(userId, token);
        if (status)
            return RedirectToAction(nameof(CartIndex));
        return View();
    }

    public async Task<IActionResult> Remove(int id)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
        var status = await _cartServices.RemoveFromCart(id, token);

        if (status)
            return RedirectToAction(nameof(CartIndex));
        return View();
    }


    private async Task<CartViewModel> FindUserCart()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

        var response = await _cartServices.FindCartByUserId(userId, token);
        if (response?.CartHeader != null)
        {
            foreach (var itemDetail in response.CartDetails)
            {
                response.CartHeader.PurchaseAmount += (itemDetail.Product.Price * itemDetail.Count);
            }
        }
        return response;
    }
}
