using System;
using System.Collections.Generic;

namespace Globalmantics.Domain
{
    public class Cart
    {
        private Cart()
        {
            CartItems = new List<CartItem>();
        }

        public int CartId { get; private set; }

        public virtual User User { get; private set; }
        public int UserId { get; private set; }

        public virtual ICollection<CartItem> CartItems { get; }

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
