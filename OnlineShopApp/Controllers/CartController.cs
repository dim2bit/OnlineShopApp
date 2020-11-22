using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Models.Interfaces;
using OnlineShopApp.Models;
using OnlineShopApp.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShopApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly Cart _cart;

        public CartController(IProductRepository productRepository, Cart cart)
        {
            _productRepository = productRepository;
            _cart = cart;
        }

        public ViewResult Index()
        {
            var cartViewModel = new CartViewModel { Cart = _cart };
            cartViewModel.Cart.GetCartItems();

            return View(cartViewModel);
        }

        public RedirectToActionResult AddToCart(int productId)
        {
            var selectedProduct = _productRepository.Products
                                  .FirstOrDefault(product => product.ProductId == productId);

            _cart.AddToCart(selectedProduct);

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromCart(int productId)
        {
            var selectedProduct = _productRepository.Products
                                  .FirstOrDefault(product => product.ProductId == productId);

            _cart.RemoveFromCart(selectedProduct);

            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearCart()
        {
            _cart.ClearCart();

            return RedirectToAction("Index");
        }
    }
}
