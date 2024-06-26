﻿using GreekShoping.OrderAPI.Data.ValueObjects;

namespace GreekShoping.OrderAPI.Repository._Cart;

public interface ICartRepository
{
    Task<CartVO> FindCartByUserId(string userId);
    Task<CartVO> SaveOrUpdateCart(CartVO vO);
    Task<bool> RemoveFromCart(long cartDetailsId);
    Task<bool> ApplyCoupon(string userId, string couponCode);
    Task<bool> RemoveCoupon(string userId);
    Task<bool> ClearCart(string userId);
}
