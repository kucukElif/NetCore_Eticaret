using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using Microsoft.AspNetCore.Mvc;
using MVC.CustomHelper;
using MVC.Models.CartModel;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }
        public IActionResult Index( )
        {
            return View(productService.GetActive());
        }

        public IActionResult AddToCart(Guid id)
        {
            Cart cartSession = SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "cart") == null ? new Cart() : SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "cart");
            var product = productService.GetById(id);
            CartItem cartItem = new CartItem();
            cartItem.ID = product.ID;
            cartItem.Name = product.ProductName;
            cartItem.ImagePath = product.ImagePath;
            cartItem.Price = product.UnitPrice;
            cartSession.AddItem(cartItem);
            SessionHelper.SetProductJson(HttpContext.Session, "cart", cartSession);
            return RedirectToAction("Index");
        }
    }
}
