using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using BLL.Repository;
using DAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.CustomHelper;
using MVC.Models.CartModel;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;

        public HomeController(IProductService productService , IOrderService orderService ,SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            this.productService = productService;
            this.orderService = orderService;
            this.signInManager = signInManager;
            this.userManager = userManager;
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
        public IActionResult MyCart()
        {
            if (SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "cart") != null)
            {
                Cart c = SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "cart");
                return View(c.MyCart);
            }
            return RedirectToAction("Index");
        }

      public async Task<IActionResult> CompleteCart()
        {
            Cart cart = SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "cart");
            Order order = new Order();
            if (signInManager.IsSignedIn(User))
            {
                var user = await userManager.GetUserAsync(User);
                order.AppUser = user;
            }
            foreach (var c in cart.MyCart)
            {
                OrderDetail od = new OrderDetail();
                var product = productService.GetById(c.ID);
                od.Product = product;
                od.UnitPrice = c.Price;
                od.Quantity = c.Quantity;
                order.OrderDetails.Add(od);


            }
            orderService.Add(order);
            return View();
        }
    }
}
