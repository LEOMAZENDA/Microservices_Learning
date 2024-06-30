using AutoMapper;
using GreekShoping.OrderAPI.Context;
using GreekShoping.OrderAPI.Data.ValueObjects;
using GreekShoping.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GreekShoping.OrderAPI.Repository._Cart;

public class CartRepository : ICartRepository
{
    private readonly MySqlContext _context;
    private IMapper _mapper;

    public CartRepository(MySqlContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<CartVO> FindCartByUserId(string userId) 
    {
        Cart cart = new ()
        {
            CartHeader = await _context.CartHeaders
            .FirstOrDefaultAsync(x => x.UserId == userId) ?? new CartHeader(),
        };
        cart.CartDetails = _context.CartDetails
            .Where(c => c.CartHeaderId == cart.CartHeader.Id)
            .Include(c => c.Product);
        
        return _mapper.Map<CartVO>(cart);
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
                p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                p.CartHeaderId == cartHeader.Id);
            if (carDetail == null)
            {
                //Create CartDetails
                cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
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

    public async Task<bool> RemoveFromCart(long cartDetailsId)
    {
        try
        {
            CartDetail cartDetail = await _context.CartDetails.FirstOrDefaultAsync(c => c.Id == cartDetailsId);
            int total = _context.CartDetails.Where(c => c.CartHeaderId == cartDetail.CartHeaderId).Count();

            _context.CartDetails.Remove(cartDetail);

            if (total == 1)
            {
                var cartHeaderToRemove = await _context.CartHeaders
                    .FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);
                _context.CartHeaders.Remove(cartHeaderToRemove);
            }
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> ClearCart(string userId)
    {
        var cartHeader = await _context.CartHeaders
            .FirstOrDefaultAsync(c => c.UserId == userId);
        if (cartHeader != null)
        {
            _context.CartDetails.RemoveRange(
                _context.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id));
            _context.CartHeaders.Remove(cartHeader);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> ApplyCoupon(string userId, string couponCode)
    {
        var header = await _context.CartHeaders
            .FirstOrDefaultAsync(c => c.UserId == userId);
        if (header != null)
        {
            header.CouponCode = couponCode;
            _context.CartHeaders.Update(header);
              await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> RemoveCoupon(string userId)
    {
        var header = await _context.CartHeaders
            .FirstOrDefaultAsync(c => c.UserId == userId);
        if (header != null)
        {
            header.CouponCode = string.Empty;
            _context.CartHeaders.Update(header);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
