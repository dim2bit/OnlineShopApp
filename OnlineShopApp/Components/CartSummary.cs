using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopApp.Models;
using OnlineShopApp.ViewModels;


namespace OnlineShopApp.Components
{
    public class CartSummary : ViewComponent
    {
        private readonly Cart _cart;

        public CartSummary(Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            _cart.GetCartItems();

            var cartViewModel = new CartViewModel
            {
                Cart = _cart
            };

            return View(cartViewModel);
        }
    }
}
