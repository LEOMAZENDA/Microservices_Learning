using GreekShoping.CartAPI.Data.ValueObjects;
using GreekShoping.CartAPI.Repository._Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreekShoping.CartAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private ICartRepository _repository;
    public CartController(ICartRepository repository)
    {
        _repository = repository ?? throw new
            ArgumentNullException(nameof(repository));
    }


    [HttpGet("find-cart{id}")]
    public async Task<ActionResult<CartVO>> FindById(string UserId)
    {
        var cart = await _repository.FindCartByUserId(UserId);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpPost("Add-cart{id}")]
    public async Task<ActionResult<CartVO>> AddCart(CartVO vO)
    {
        var cart = await _repository.SaveOrUpdateCart(vO);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpPut("update-cart{id}")]
    public async Task<ActionResult<CartVO>> UpdateCart(CartVO vO)
    {
        var cart = await _repository.SaveOrUpdateCart(vO);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpDelete("remove-cart{id}")]
    public async Task<ActionResult<bool>> RemoveCart(int id)
    {
        var status = await _repository.RemoveFromCart(id);
        if (!status) return BadRequest();
        return Ok(status);
    }
}
