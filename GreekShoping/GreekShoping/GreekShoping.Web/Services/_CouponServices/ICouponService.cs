using GreekShoping.Web.Models;

namespace GreekShoping.Web.Services._CouponServices;

public interface ICouponService
{
    Task<CouponViewModel> GetCoupon (string code, string token);
}
