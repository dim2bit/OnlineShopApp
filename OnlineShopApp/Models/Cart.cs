using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopApp.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public List<CartItem> CartItems { get; set; }

        private AppDbContext _appDbContext;

        private static int _cartIdCounter { get; set; } = -1;

        public Cart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                               .HttpContext.Session; // get current session

            string cartId = session.GetString("CartId") ?? _cartIdCounter++.ToString(); // get session CartId string or create new
            session.SetString("CartId", cartId);
            return new Cart(services.GetService<AppDbContext>()) { CartId = Int32.Parse(cartId) };
        }

        public List<CartItem> GetCartItems()
        {
            CartItems = _appDbContext.CartItems
                        .Where(item => item.CartId == CartId)
                        .Include(item => item.Product)
                        .ToList();

            return CartItems;
        }

        public float GetTotalPrice()
        {
            float totalPrice = _appDbContext.CartItems
                                .Where(item => item.CartId == CartId)
                                .Select(item => item.Product.Price * item.Quantity)
                                .Sum();
            return totalPrice;
        }

        public int GetTotalProductQuantity()
        {
            return CartItems.Select(item => item.Quantity).Sum();
        }

        public void AddToCart(Product product)
        {
            var cartItem = _appDbContext.CartItems.SingleOrDefault(
                                item => item.Product.ProductId == product.ProductId
                                     && item.CartId == CartId);

            if (cartItem == null)  // if there is no same product already added
            {
                cartItem = new CartItem
                {
                    CartId = this.CartId,
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
                                     && item.CartId == CartId);

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
                            .Where(item => item.CartId == CartId);

            if (cartItems != null)
            {
                _appDbContext.CartItems.RemoveRange(cartItems);

                _appDbContext.SaveChanges();
            }
        }
    }
}
