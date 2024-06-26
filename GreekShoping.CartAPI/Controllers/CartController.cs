using GreekShoping.CartAPI.Data.ValueObjects;
using GreekShoping.CartAPI.Message;
using GreekShoping.CartAPI.RabbitMQCenter;
using GreekShoping.CartAPI.Repository._Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreekShoping.CartAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private ICartRepository _repository;
    private IRabbitMQMessageSender _rabbitMQMessageSender;

    public CartController(ICartRepository repository, IRabbitMQMessageSender rabbitMQMessageSender)
    {
        _repository = repository ?? 
            throw new ArgumentNullException(nameof(repository));
        _rabbitMQMessageSender = rabbitMQMessageSender ?? 
            throw new ArgumentNullException(nameof(rabbitMQMessageSender));
    }

    [HttpGet("find-cart/{id}")]
    public async Task<ActionResult<CartVO>> FindById(string id)
    {
        var cart = await _repository.FindCartByUserId(id);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpPost("add-cart")]
    public async Task<ActionResult<CartVO>> AddCart(CartVO vO)
    {
        var cart = await _repository.SaveOrUpdateCart(vO);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpPut("update-cart")]
    public async Task<ActionResult<CartVO>> UpdateCart(CartVO vO)
    {
        var cart = await _repository.SaveOrUpdateCart(vO);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpDelete("remove-cart/{id}")]
    public async Task<ActionResult<bool>> RemoveCart(int id)
    {
        var status = await _repository.RemoveFromCart(id);
        if (!status) return BadRequest();
        return Ok(status);
    }

    [HttpPost("apply-coupon")]
    public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vO)
    {
        var stattus = await _repository.ApplyCoupon(vO.CartHeader.UserId, vO.CartHeader.CouponCode);
        if (!stattus) return NotFound();
        return Ok(stattus);
    }

    [HttpDelete("remove-coupon/{userId}")]
    public async Task<ActionResult<CartVO>> RemoveCoupon(string userId)
    {
        var stattus = await _repository.RemoveCoupon(userId);
        if (!stattus) return NotFound();
        return Ok(stattus);
    }

    [HttpPost("checkout")]
    public async Task<ActionResult<CheckouHeaderVO>> Checkout(CheckouHeaderVO vO)
    {
        if(vO?.UserId == null) return NotFound();
        var cart = await _repository.FindCartByUserId(vO.UserId);
        if (cart == null) return NotFound();
        vO.CartDetails = cart.CartDetails;
        vO.DateTime = DateTime.Now;

        //TASK RabbititMQ Logic comes here!!!
        _rabbitMQMessageSender.SendeMessage(vO, "checkoutqueue22");
        return Ok(vO);
    }
}
