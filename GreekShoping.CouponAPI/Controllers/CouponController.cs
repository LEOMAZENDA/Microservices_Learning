using GreekShoping.CouponAPI.Data.ValueObjects;
using GreekShoping.CouponAPI.Repository._Coupon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreekShoping.CouponAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private ICouponRepository _repository;
        public CouponController(ICouponRepository repository)
        {
            _repository = repository ?? throw new
                ArgumentNullException(nameof(repository));
        }

        

        [HttpGet("{couponCode}")]
        [Authorize]
        public async Task<ActionResult<CouponVO>> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _repository.GetCouponByCouponCode(couponCode);
            if (coupon.Id <= 0) return NotFound();
            return Ok(coupon);
        }
    }
}
