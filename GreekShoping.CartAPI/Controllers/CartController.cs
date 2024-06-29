using GreekShoping.CartAPI.Data.ValueObjects;
using GreekShoping.CartAPI.Message;
using GreekShoping.CartAPI.RabbitMQCenter;
using GreekShoping.CartAPI.Repository._Cart;
using GreekShoping.CartAPI.Repository._Coupon;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreekShoping.CartAPI.Controllers;

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
        _cartRepository = cartRepository ?? 
            throw new ArgumentNullException(nameof(cartRepository));
        _couponRepository = couponRepository ?? 
            throw new ArgumentNullException(nameof(couponRepository));
        _rabbitMQMessageSender = rabbitMQMessageSender ?? 
            throw new ArgumentNullException(nameof(rabbitMQMessageSender));
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
        var token = await HttpContext.GetTokenAsync("access_token");
        if (vO?.UserId == null) return NotFound("Teste de não encontrado");
        var cart = await _cartRepository.FindCartByUserId(vO.UserId);
        if (cart == null) return NotFound();
        if(!string.IsNullOrEmpty(vO.CouponCode))
        {
            CouponVO coupon = await _couponRepository.GetCoupon(vO.CouponCode, token);

            if (vO.DiscountAmount != coupon.DiscountAmount)
            {
                return StatusCode(412); 
                //412 significa que já mudou as pré-condições antes estabelecidas
                //(no caso, o preço do coupon já não é o mesmo mesmo que o cliente viu antes de seleciona-lo no carrinho) 
            }
        }
        vO.CartDetails = cart.CartDetails;
        vO.DateTime = DateTime.Now;

        //TASK RabbititMQ Logic comes here!!!
        _rabbitMQMessageSender.SendeMessage(vO, "checkoutqueue");
        return Ok(vO);
    }
}
