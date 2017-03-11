using System;
using Globalmantics.Domain;

namespace Globalmantics.Domain
{
    public class CartItem
    {
        private CartItem()
        {
        }

        public int CartItemId { get; private set; }

        public Cart Cart { get; private set; }
        public int CartId { get; private set; }

        public CatalogItem CatalogItem { get; private set; }
        public int CatalogItemId { get; private set; }

        public int Quantity { get; private set; }

        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public static CartItem Create(CatalogItem catalogItem)
        {
            return new CartItem
            {
                CatalogItem = catalogItem,
                Quantity = 0
            };
        }
    }
}
