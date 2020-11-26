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

        private static int _cartIdCounter { get; set; } = -1;

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                               .HttpContext.Session; // get current session

            string cartId = session.GetString("CartId") ?? _cartIdCounter++.ToString(); // get session CartId string or create new
            session.SetString("CartId", cartId);

            return new Cart { CartId = Int32.Parse(cartId) };
        }
    }
}
