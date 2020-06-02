using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Areas.Admin.Models.ViewModels;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductController(IProductService productService,ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            ProductVM productVM = new ProductVM();
            productVM.Products = productService.GetActive();
            productVM.Categories = categoryService.GetActive();
            return View(productVM);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.Categories = categoryService.GetActive().Select(x => new SelectListItem() { Text = x.CategoryName, Value = x.ID.ToString() });

            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product, IFormFile image)
        {
            try
            {
                string path;
                if (image==null)
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", "noimage.jpg");
                    product.ImagePath = "noimage.jpg";


                }
                else
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), image.FileName);
                    using (var stream = new FileStream(path, FileMode.Create)) {
                      await image.CopyToAsync(stream);
                    }
                    product.ImagePath = image.FileName;
                }
                productService.Add(product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(Guid id)
        {
            ViewBag.Categories = categoryService.GetActive().Select(x => new SelectListItem() { Text = x.CategoryName, Value = x.ID.ToString() });
            return View(productService.GetById(id));
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product, IFormFile image )
        {
            try
            {
                string path;
                if (image == null)
                {
                    if (product.ImagePath!=null)
                    {
                        productService.Update(product);
                        return RedirectToAction("Index");
                    }
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", "noimage.jpg");
                    product.ImagePath = "noimage.jpg";


                }
                else
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), image.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    product.ImagePath = image.FileName;
                }
                productService.Update(product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(productService.GetById(id));
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product product)
        {
            try
            {
                productService.Remove(product.ID);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
