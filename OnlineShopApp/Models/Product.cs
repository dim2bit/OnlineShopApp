using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string ImageUrl { get; set; }

        public bool IsInWishlist { get; set; }

        public bool IsInStock { get; set; }

        public Category Category { get; set; }
    }
}
