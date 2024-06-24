using GreekShoping.Web.Models;
using GreekShoping.Web.Services._CartServices;
using GreekShoping.Web.Services._ProductServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GreekShoping.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICartServices _cartServices;

    public HomeController(ILogger<HomeController> logger,
        IProductService productService,
        ICartServices cartServices)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _cartServices = cartServices ?? throw new ArgumentNullException(nameof(cartServices)); 
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.FindAllProducts("");
        return View(products);
    }

    
    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var model = await _productService.FindAllProductById(id, accessToken);
        return View(model);
    }

    [Authorize]
    public async Task<IActionResult> Login()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        return RedirectToAction(nameof(Index));
    }


    [ActionName("Details")]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DetailsPost(ProductViewModel model)
    {
        var token = await HttpContext.GetTokenAsync("access_token");

        CartViewModel cart = new()
        {
            CartHeader = new CartHeaderViewModel
            {
                UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
            }
        };

        CartDetailViewModel cartDetail = new CartDetailViewModel()
        {
            Count = model.Count,
            ProductId = model.Id,
            Product = await _productService.FindAllProductById(model.Id, token)
        };

        List<CartDetailViewModel> cartDetails = new List<CartDetailViewModel>();
        cartDetails.Add(cartDetail);
        cart.CartDetails = cartDetails;

        var response = await _cartServices.AddItemToCart(cart, token);
        if (response != null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }




    public IActionResult Logout()
    {
        return SignOut("Cookies","oidc");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
