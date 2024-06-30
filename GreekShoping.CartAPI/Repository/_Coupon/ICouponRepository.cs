using GreekShoping.CartAPI.Data.ValueObjects;

namespace GreekShoping.CartAPI.Repository._Coupon
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCoupon(string couponCode, string token);
    }
}
