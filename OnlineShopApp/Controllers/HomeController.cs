using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Models;
using OnlineShopApp.Models.Interfaces;
using OnlineShopApp.ViewModels;


namespace OnlineShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categories;
        private readonly IProductRepository _products;

        public HomeController(ICategoryRepository categories, IProductRepository products)
        {
            _categories = categories;
            _products = products;
        }

        public ViewResult Index(string category)
        {
            ViewBag.Title = "Products";

            IEnumerable<Product> products;

            if (category == "AllCategories")
            {
                products = _products.Products;
                category = "All categories";
            }
            else if (_categories.Categories
                     .FirstOrDefault(categ => categ.Name == category) == null)
            {
                products = null;
                category = "Not found";
            }
            else
            {
                products = _products.Products
                           .Where(product => product.Category.Name == category);
            }

            var productListVm = new ProductListViewModel
            {
                Products = products,
                CurrentCategory = category
            };

            return View(productListVm);
        }
    }
}
