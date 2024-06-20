using GreekShoping.ProductApi.Data.ValueObjects;
using GreekShoping.ProductApi.Repository._Product;
using Microsoft.AspNetCore.Mvc;

namespace GreekShoping.ProductApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private IProductRepository _repository;
    public ProductController(IProductRepository repository)
    {
        _repository = repository ?? throw new 
            ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
    {
        var prod = await _repository.FindAll();
        return Ok(prod);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductVO>> FindById(long id)
    {
        var prod = await _repository.FindById(id);
        if (prod == null) return NotFound();
        return Ok(prod);
    }
}
