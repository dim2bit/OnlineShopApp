using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Models.Interfaces;
using OnlineShopApp.Models;
using OnlineShopApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;


namespace OnlineShopApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly Cart _cart;

        public CartController(IDistributedCache distributedCache,
                              IProductRepository productRepository,
                              ICartRepository cartRepository,
                              Cart cart)
        {
            _distributedCache = distributedCache;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _cart = cart;
        }

        [Route("cart")]
        public ViewResult Index()
        {
            var cartViewModel = new CartViewModel()
            {
                Cart = _cart,
                CartRepository = _cartRepository
            };

            if (string.IsNullOrEmpty(_distributedCache.GetString("cart")))
            {
                List<CartItem> cartList = _cartRepository.GetCartItems();

                string cartString = JsonConvert.SerializeObject(cartList);

                _distributedCache.SetString("cart", cartString);
            }
            else
            {
                string cartFromCache = _distributedCache.GetString("cart");

                cartViewModel.Cart.CartItems = JsonConvert.DeserializeObject<List<CartItem>>(cartFromCache);

                _distributedCache.SetString("cart", cartFromCache);
            }

            return View(cartViewModel);
        }
        
        [Route("cart/add/{productId?}")]
        public RedirectToActionResult AddToCart(int productId)
        {
            var selectedProduct = _productRepository.Products
                                  .FirstOrDefault(product => product.ProductId == productId);

            _cartRepository.AddToCart(selectedProduct);
            _distributedCache.SetString("cart", "");

            return RedirectToAction("Index");
        }

        [Route("cart/remove/{productId?}")]
        public RedirectToActionResult RemoveFromCart(int productId)
        {
            var selectedProduct = _productRepository.Products
                                  .FirstOrDefault(product => product.ProductId == productId);

            _cartRepository.RemoveFromCart(selectedProduct);
            _distributedCache.SetString("cart", "");

            return RedirectToAction("Index");
        }

        [Route("cart/clear")]
        public RedirectToActionResult ClearCart()
        {
            _cartRepository.ClearCart();
            _distributedCache.SetString("cart", "");

            return RedirectToAction("Index");
        }
    }
}
