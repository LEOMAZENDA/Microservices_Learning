using AutoMapper;
using GreekShoping.CartAPI.Data.ValueObjects;
using GreekShoping.CartAPI.Models.Context;

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

    public Task<bool> ApplyCoupon(string userId, string couponCode)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ClearCart(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<CartVO> FindCartById(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveCoupon(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveFromCart(long cartDetails)
    {
        throw new NotImplementedException();
    }

    public Task<CartVO> SaveOrUpdateCart(CartVO cart)
    {
        throw new NotImplementedException();
    }
}
