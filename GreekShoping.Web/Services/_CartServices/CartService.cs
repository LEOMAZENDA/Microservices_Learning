﻿using GreekShoping.Web.Models;
using GreekShoping.Web.Utils;
using System.Net.Http.Headers;
using System.Reflection;

namespace GreekShoping.Web.Services._CartServices;

public class CartService : ICartService
{
    private readonly HttpClient _client;
    public const string BasePath = "api/v1/cart";

    public CartService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<CartViewModel> FindCartByUserId(string iserId, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.GetAsync($"{BasePath}/find-cart/{iserId}");
        return await response.ReadContentAs<CartViewModel>();
    }

    public async Task<CartViewModel> AddItemToCart(CartViewModel model, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.PostAsJson($"{BasePath}/add-cart", model);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<CartViewModel>();
        else throw new Exception("Something went wrong when calling API");
    }

    public async Task<CartViewModel> UpdateCart(CartViewModel model, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.PutAsJson($"{BasePath}/update-cart", model);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<CartViewModel>();
        else throw new Exception("Something went wrong when calling API");
    }

    public async Task<bool> RemoveFromCart(long cartId, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.DeleteAsync($"{BasePath}/remove-cart/{cartId}");
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<bool>();
        else throw new Exception("Something went wrong when calling API");
    }

    public async Task<bool> ApplyCoupon(CartViewModel model, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.PostAsJson($"{BasePath}/apply-coupon",model);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<bool>();
        else throw new Exception("Something went wrong when calling API");
    }

    public async Task<bool> RemoveCoupon(string userId, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.DeleteAsync($"{BasePath}/remove-coupon/{userId}");
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<bool>();
        else throw new Exception("Something went wrong when calling API");
    }

    public async Task<object> CheckOut(CartHeaderViewModel model, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.PostAsJson($"{BasePath}/checkout", model);
        if (response.IsSuccessStatusCode)
        {
            return await response.ReadContentAs<CartHeaderViewModel>();
        }
        else if(response.StatusCode.ToString().Equals("PreconditionFailed"))
        {
            return "Coupon price has changed. Pleasse Confirm!";
        }
        else throw new Exception("Something went wrong when calling API");
    }

    public async Task<bool> ClearCart(string iserId, string token)
    {
        throw new NotImplementedException();
    }    
}