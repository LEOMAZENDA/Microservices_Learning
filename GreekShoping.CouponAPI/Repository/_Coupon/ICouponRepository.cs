using GreekShoping.CouponAPI.Data.ValueObjects;

namespace GreekShoping.CouponAPI.Repository._Coupon
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string couponCode);
    }
}
