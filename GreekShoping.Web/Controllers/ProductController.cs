using GreekShoping.Web.Models;
using GreekShoping.Web.Services._ProductServices;
using GreekShoping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace GreekShoping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProductIndex()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var products = await _productService.FindAllProducts(token);
            return View(products);
        }

        [HttpGet]
        public IActionResult ProductCreate()
        {
            return View();
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.CreateProduct(model, token);
                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductUpdate(long id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var model = await _productService.FindAllProductById(id, token);
            if (model != null) return View(model);
            return NotFound();
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.UpdateProduct(model, token);
                if (response != null) 
                    return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProductDelete(long id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var model = await _productService.FindAllProductById(id, token);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        [Authorize(Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductById(model.Id, token);
                if (response) 
                    return RedirectToAction(nameof(ProductIndex));
            return View(model);
        }
    }
}
