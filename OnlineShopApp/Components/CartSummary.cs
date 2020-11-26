using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopApp.Models;
using OnlineShopApp.Models.Interfaces;
using OnlineShopApp.ViewModels;


namespace OnlineShopApp.Components
{
    public class CartSummary : ViewComponent
    {
        private readonly ICartRepository _cartRepository;
        private readonly Cart _cart;

        public CartSummary(ICartRepository cartRepository, Cart cart)
        {
            _cartRepository = cartRepository;
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            _cart.CartItems = _cartRepository.GetCartItems();

            var cartViewModel = new CartViewModel
            {
                Cart = _cart,
                CartRepository = _cartRepository
            };

            return View(cartViewModel);
        }
    }
}
