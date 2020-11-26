using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShopApp.Models.Interfaces;


namespace OnlineShopApp.Models.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly Cart _cart;

        public CartRepository(AppDbContext appDbContext, Cart cart)
        {
            _appDbContext = appDbContext;
            _cart = cart;
        }

        public List<CartItem> GetCartItems()
        {
            _cart.CartItems = _appDbContext.CartItems
                        .Where(item => item.CartId == _cart.CartId)
                        .Include(item => item.Product)
                        .ToList();

            return _cart.CartItems;
        }

        public float GetTotalPrice()
        {
            return _appDbContext.CartItems
                   .Where(item => item.CartId == _cart.CartId)
                   .Select(item => item.Product.Price * item.Quantity)
                   .Sum();
        }

        public float GetItemPrice(int itemId)
        {
            return _appDbContext.CartItems
                   .Where(item => item.CartId == _cart.CartId)
                   .Where(item => item.ItemId == itemId)
                   .Select(item => item.Product.Price * item.Quantity)
                   .Sum();
        }

        public int GetTotalProductQuantity()
        {
            return _cart.CartItems.Select(item => item.Quantity).Sum();
        }

        public void AddToCart(Product product)
        {
            var cartItem = _appDbContext.CartItems.SingleOrDefault(
                                item => item.Product.ProductId == product.ProductId
                                     && item.CartId == _cart.CartId);

            if (cartItem == null)  // if there is no same product already added
            {
                cartItem = new CartItem
                {
                    CartId = _cart.CartId,
                    Quantity = 1,
                    Product = product
                };

                _appDbContext.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            _appDbContext.SaveChanges();
        }

        public void RemoveFromCart(Product product)
        {
            var cartItem = _appDbContext.CartItems.SingleOrDefault(
                                item => item.Product.ProductId == product.ProductId
                                     && item.CartId == _cart.CartId);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    _appDbContext.CartItems.Remove(cartItem);
                }

                _appDbContext.SaveChanges();
            }
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext.CartItems
                            .Where(item => item.CartId == _cart.CartId);

            if (cartItems != null)
            {
                _appDbContext.CartItems.RemoveRange(cartItems);

                _appDbContext.SaveChanges();
            }
        }
    }
}
