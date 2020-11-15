using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShopApp.Models.Interfaces;


namespace OnlineShopApp.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Product> Products => _appDbContext.Products.Include(product => product.Category);
         
        public Product GetProductById(int productId)
        {
            return _appDbContext.Products.FirstOrDefault(product => product.ProductId == productId);
        }
    }
}
