using System;
using System.Collections.Generic;

namespace Globalmantics.Domain
{
    public class Cart
    {
        public int CartId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public ICollection<CartItem> CartItems { get; set; } =
            new List<CartItem>();

        public DateTime CreatedAt { get; set; }

        public void AddItem(CatalogItem catalogItem, int quantity)
        {
            CartItems.Add(new CartItem
            {
                CatalogItem = catalogItem,
                Quantity = quantity
            });
        }
    }
}
