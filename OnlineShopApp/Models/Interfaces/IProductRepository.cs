using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopApp.Models;

namespace OnlineShopApp.Models.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        Product GetProductById(int productId);
    }
}
