using System;
using Globalmantics.Domain;
using Highway.Data;
using System.Linq;

namespace Globalmantics.Domain
{
    public class CartItem : IIdentifiable<int>
    {
        int IIdentifiable<int>.Id
        {
            get { return CartItemId; }
            set { CartItemId = value; }
        }

        private CartItem()
        {
        }

        public int CartItemId { get; private set; }

        public Cart Cart { get; private set; }
        public int CartId { get; private set; }

        public CatalogItem CatalogItem { get; private set; }
        public int CatalogItemId { get; private set; }

        public int Quantity { get; private set; }
        public decimal ItemTotal { get; private set; }

        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
            decimal discount = ComputeDiscount();
            ItemTotal = CatalogItem.UnitPrice * Quantity * (1.0m - discount);
        }

        private decimal ComputeDiscount()
        {
            return 0.0m;
        }

        public static CartItem Create(Cart cart, CatalogItem catalogItem)
        {
            return new CartItem
            {
                Cart = cart,
                CatalogItem = catalogItem,
                Quantity = 0
            };
        }
    }
}
