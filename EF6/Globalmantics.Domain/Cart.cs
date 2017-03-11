using System;
using System.Collections.Generic;

namespace Globalmantics.Domain
{
    public class Cart
    {
        private Cart()
        {
        }

        public int CartId { get; private set; }

        public User User { get; private set; }
        public int UserId { get; private set; }

        public ICollection<CartItem> CartItems { get; private set; } =
            new List<CartItem>();

        public DateTime CreatedAt { get; private set; }

        public void AddItem(CatalogItem catalogItem, int quantity)
        {
            CartItems.Add(new CartItem
            {
                CatalogItem = catalogItem,
                Quantity = quantity
            });
        }

        public static Cart Create(int userId)
        {
            return new Cart
            {
                UserId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }
}
