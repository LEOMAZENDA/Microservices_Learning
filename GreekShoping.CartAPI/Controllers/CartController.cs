using GreekShoping.OrderAPI.Data.ValueObjects;
using GreekShoping.OrderAPI.Message;
using GreekShoping.OrderAPI.RabbitMQSender;
using GreekShoping.OrderAPI.Repository._Cart;
using GreekShoping.OrderAPI.Repository._Coupon;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GreekShoping.OrderAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private ICartRepository _cartRepository;
    private ICouponRepository _couponRepository;

    private IRabbitMQMessageSender _rabbitMQMessageSender;

    public CartController(ICartRepository cartRepository, 
        ICouponRepository couponRepository, 
        IRabbitMQMessageSender rabbitMQMessageSender)
    {
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
        _rabbitMQMessageSender = rabbitMQMessageSender ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
    }

    [HttpGet("find-cart/{id}")]
    public async Task<ActionResult<CartVO>> FindById(string id)
    {
        var cart = await _cartRepository.FindCartByUserId(id);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpPost("add-cart")]
    public async Task<ActionResult<CartVO>> AddCart(CartVO vO)
    {
        var cart = await _cartRepository.SaveOrUpdateCart(vO);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpPut("update-cart")]
    public async Task<ActionResult<CartVO>> UpdateCart(CartVO vO)
    {
        var cart = await _cartRepository.SaveOrUpdateCart(vO);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpDelete("remove-cart/{id}")]
    public async Task<ActionResult<bool>> RemoveCart(int id)
    {
        var status = await _cartRepository.RemoveFromCart(id);
        if (!status) return BadRequest();
        return Ok(status);
    }

    [HttpPost("apply-coupon")]
    public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vO)
    {
        var stattus = await _cartRepository.ApplyCoupon(vO.CartHeader.UserId, vO.CartHeader.CouponCode);
        if (!stattus) return NotFound();
        return Ok(stattus);
    }

    [HttpDelete("remove-coupon/{userId}")]
    public async Task<ActionResult<CartVO>> RemoveCoupon(string userId)
    {
        var stattus = await _cartRepository.RemoveCoupon(userId);
        if (!stattus) return NotFound();
        return Ok(stattus);
    }

    [HttpPost("checkout")]
    public async Task<ActionResult<CheckouHeaderVO>> Checkout(CheckouHeaderVO vO)
    {
        //string token = Request.Headers["Autorization"];
        var token = await HttpContext.GetTokenAsync("access_token");

        if (vO?.UserId == null) return NotFound("Teste de não encontrado");
        var cart = await _cartRepository.FindCartByUserId(vO.UserId);
        if (cart == null) return NotFound();
        vO.CartDetails = cart.CartDetails;
        vO.DateTime = DateTime.Now;

        if (!string.IsNullOrEmpty(vO.CouponCode))
        {
            CouponVO coupon = await _couponRepository.GetCoupon(vO.CouponCode, token);
            if (vO.DiscountAmount != coupon.DiscountAmount) 
                return StatusCode(412); //Preconditoin Failed (no Caso, o valor do cuopon mudou)
        }
        vO.CartDetails = cart.CartDetails;
        vO.DateTime = DateTime.Now;

        //TASK RabbititMQ Logic comes here!!!
        _rabbitMQMessageSender.SendeMessage(vO, "checkoutqueue");

        await _cartRepository.ClearCart(vO.UserId);
        return Ok(vO);
    }
}
