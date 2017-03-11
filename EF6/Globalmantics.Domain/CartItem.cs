using Globalmantics.Domain;

namespace Globalmantics.Domain
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        public Cart Cart { get; set; }
        public int CartId { get; set; }

        public CatalogItem CatalogItem { get; set; }
        public int CatalogItemId { get; set; }

        public int Quantity { get; set; }
    }
}
