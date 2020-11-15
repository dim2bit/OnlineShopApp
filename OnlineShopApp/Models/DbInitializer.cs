using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OnlineShopApp.Models
{
    public class DbInitializer
    {
        private static Dictionary<string, Category> _categories;

        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    var categoryList = new Category[]
                    {
                        new Category { Name = "Laptops", Description = "Laptop computers" },
                        new Category { Name = "Smartphones", Description = "Smartphone devices" }
                    };

                    _categories = new Dictionary<string, Category>();

                    foreach (var category in categoryList)
                    {
                        _categories.Add(category.Name, category);
                    }
                }

                return _categories;
            }
        }
        
        public static void Initialize(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(category => category.Value));
            }

            context.CartItems.RemoveRange(context.CartItems);
            context.Products.RemoveRange(context.Products);
            context.Categories.RemoveRange(context.Categories);
            context.SaveChanges();

            context.AddRange
                (
                    new Product
                    {
                        Name = "Asus Nitro 5",
                        Description = "Gaming laptop",
                        Price = 25000,
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcTxoyLE1lNxiJvMBoW7v_MP3t-vxIlP8AaJFHrnYxm0PfRQKxfBW9COj2d8N4kbIBxquqVnS2iB&usqp=CAc",
                        IsInWishlist = true,
                        IsInStock = true,
                        Category = Categories["Laptops"]
                    },
                    new Product
                    {
                        Name = "Xiaomi Mi9t",
                        Description = "One of the most popular smartphones in the world",
                        Price = 6000,
                        ImageUrl = "https://www.xiaomitoday.it/wp-content/uploads/2019/05/xiaomi-mi-9t-1.jpg",
                        IsInWishlist = true,
                        IsInStock = true,
                        Category = Categories["Smartphones"]
                    },
                    new Product
                    {
                        Name = "iPhone X",
                        Description = "New flagship Apple device",
                        Price = 10000,
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcS2oLdSHkEZPsYsV6M7bTH2oDPR5C-iV1UCHg&usqp=CAU",
                        IsInWishlist = true,
                        IsInStock = true,
                        Category = Categories["Smartphones"]
                    },
                    new Product
                    {
                        Name = "Note 10",
                        Description = "New flagship Samsung device",
                        Price = 20000,
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcTvTySQMgpREU_gTim7eo_Ore6V_gp4_uhcIRAl3XiqVdbhG2VLFWZ4AguGGQ&usqp=CAc",
                        IsInWishlist = true,
                        IsInStock = true,
                        Category = Categories["Smartphones"]
                    },
                    new Product
                    {
                        Name = "Zenbook 14",
                        Description = "Office laptop",
                        Price = 10000,
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcQEjUU-aSIydpC4BKr1KNOwfEl3bI7xGVDjCUJPPDK2aiGLKBb8yRffWdJz6LqyTMUch6LjoLY&usqp=CAc",
                        IsInWishlist = true,
                        IsInStock = true,
                        Category = Categories["Laptops"]
                    }
                );
            

            context.SaveChanges();
        }
    }
}
    