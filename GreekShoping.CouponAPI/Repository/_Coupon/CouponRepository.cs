using AutoMapper;
using GreekShoping.CouponAPI.Data.ValueObjects;
using GreekShoping.CouponAPI.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace GreekShoping.CouponAPI.Repository._Coupon;

public class CouponRepository : ICouponRepository
{
    private readonly MySqlContext _context;
    private IMapper _mapper;

    public CouponRepository(MySqlContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<CouponVO> GetCouponByCouponCode(string couponCode)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
        return _mapper.Map<CouponVO>(coupon);
    }
}
