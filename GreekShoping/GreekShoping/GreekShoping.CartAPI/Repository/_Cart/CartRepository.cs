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

        //Checks if the product is already saved in the database, inf doesn´t exist then save
        var product = await _context.Products.FirstOrDefaultAsync(
            p => p.Id == vO.CartDetails.FirstOrDefault().ProductId);

        if (product == null)
        {
            _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
            await _context.SaveChangesAsync();
        }

        //Chech if CartHeader is null
        var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
            c => c.UserId == cart.CartHeader.UserId);

        if (cartHeader == null)
        {
            //Create cartHeader and Details
            _context.CartHeaders.Add(cart.CartHeader);
            await _context.SaveChangesAsync();

            cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
            cart.CartDetails.FirstOrDefault().Product = null;
            _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
            await _context.SaveChangesAsync();
        }
        else
        {
            //If CartHeader isn´t null 
            //Check if CartDetails has same Produxt
            var carDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                p => p.ProductId == vO.CartDetails.FirstOrDefault().ProductId &&
                p.CartHeaderId == cartHeader.Id);
            if (carDetail == null)
            {
                //Create CartDetails
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                //Update product count and CartDetails
                cart.CartDetails.FirstOrDefault().Product = null;
                cart.CartDetails.FirstOrDefault().Count += carDetail.Count;
                cart.CartDetails.FirstOrDefault().Id = carDetail.Id;
                cart.CartDetails.FirstOrDefault().CartHeaderId = carDetail.CartHeaderId;
                _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
        }
        return _mapper.Map<CartVO>(cart);
    }
}
