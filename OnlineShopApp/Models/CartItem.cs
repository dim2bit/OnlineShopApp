using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopApp.Models
{
    public class CartItem
    {
        [Key]
        public int ItemId { get; set; }

        public int CartId { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }

        public float GetTotalPrice()
        {
            return Product.Price * Quantity;
        }
    }   
}
