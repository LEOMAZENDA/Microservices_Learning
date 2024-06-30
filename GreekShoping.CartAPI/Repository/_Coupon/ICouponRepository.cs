using GreekShoping.OrderAPI.Data.ValueObjects;

namespace GreekShoping.OrderAPI.Repository._Coupon
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCoupon(string couponCode, string token);
    }
}
