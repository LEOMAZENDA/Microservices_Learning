using AutoMapper;
using GreekShoping.CartAPI.Data.ValueObjects;
using GreekShoping.CartAPI.Models;
using GreekShoping.CartAPI.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace GreekShoping.CartAPI.Repository._Cart;

public class CartRepository : ICartRepository
{
    private readonly MySqlContext _context;
    private IMapper _mapper;

    public CartRepository(MySqlContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ApplyCoupon(string userId, string couponCode)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ClearCart(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<CartVO> FindCartById(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveCoupon(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveFromCart(long cartDetails)
    {
        throw new NotImplementedException();
    }

    public async Task<CartVO> SaveOrUpdateCart(CartVO vO)
    {
        Cart cart = _mapper.Map<Cart>(vO);
        var product = await _context.Products.FirstOrDefaultAsync(
            p => p.Id == vO.CartDetails.FirstOrDefault().ProductId);

        if (product == null) {
            _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
            await _context.SaveChangesAsync();
        }

        var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
            c => c.UserId == cart.CartHeader.UserId);

        if (cartHeader == null) {
            _context.CartHeaders.Add(cart.CartHeader);
            await _context.SaveChangesAsync();

            cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
            cart.CartDetails.FirstOrDefault().Product = null;
            _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
            await _context.SaveChangesAsync();
        }
    }
}
