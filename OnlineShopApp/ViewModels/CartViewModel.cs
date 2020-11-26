using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopApp.Models;
using OnlineShopApp.Models.Interfaces;


namespace OnlineShopApp.ViewModels
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }

        public ICartRepository CartRepository { get; set; }
    }
}
