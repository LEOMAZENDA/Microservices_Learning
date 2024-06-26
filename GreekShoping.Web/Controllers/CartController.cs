﻿using GreekShoping.Web.Models;
using GreekShoping.Web.Services._CartServices;
using GreekShoping.Web.Services._CouponServices;
using GreekShoping.Web.Services._ProductServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace GreekShoping.Web.Controllers;

public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ICouponService _couponService;

    public CartController(
        IProductService productService,
        ICartService cartServices,
        ICouponService couponServices)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _cartService = cartServices ?? throw new ArgumentNullException(nameof(cartServices));
        _couponService = couponServices ?? throw new ArgumentNullException(nameof(couponServices));
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

        var status = await _cartService.ApplyCoupon(model, token);
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

        var status = await _cartService.RemoveCoupon(userId, token);
        if (status)
            return RedirectToAction(nameof(CartIndex));
        return View();
    }

    public async Task<IActionResult> Remove(int id)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
        var status = await _cartService.RemoveFromCart(id, token);

        if (status)
            return RedirectToAction(nameof(CartIndex));
        return View();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        return View(await FindUserCart());
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(CartViewModel model)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var response = await _cartService.CheckOut(model.CartHeader, token);

        if (response != null && response.GetType() == typeof(string))
        {
            TempData["Error"] = response;
            return RedirectToAction(nameof(Checkout));
        }
        else if (response != null)
        {
            return RedirectToAction(nameof(Confirmation));
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Confirmation()
    {
        return View();
    }


    private async Task<CartViewModel> FindUserCart()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

        var response = await _cartService.FindCartByUserId(userId, token);

        if (response?.CartHeader != null)
        {
            if (!string.IsNullOrEmpty(response.CartHeader.CouponCode))
            {
                var coupon = await _couponService.
                    GetCoupon(response.CartHeader.CouponCode, token);
                if (coupon?.CouponCode != null)
                {
                    response.CartHeader.DiscountAmount = coupon.DiscountAmount;
                }
            }
            foreach (var detail in response.CartDetails)
            {
                response.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
            }
            response.CartHeader.PurchaseAmount -= response.CartHeader.DiscountAmount;
        }
        return response;
    }

}
