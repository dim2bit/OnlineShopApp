using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopApp.Models.Interfaces
{
    public interface ICartRepository
    {
        List<CartItem> GetCartItems();

        float GetTotalPrice();

        float GetItemPrice(int itemId);

        int GetTotalProductQuantity();

        void AddToCart(Product product);

        void RemoveFromCart(Product product);

        void ClearCart();
    }
}
