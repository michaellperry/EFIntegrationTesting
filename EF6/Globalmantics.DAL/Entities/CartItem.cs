namespace Globalmantics.DAL.Entities
{
    public class CartItem
    {
        public int ItemId { get; set; }

        public Cart Cart { get; set; }
        public int CartId { get; set; }

        public CatalogItem CatalogItem { get; set; }
        public int CatalogItemId { get; set; }

        public int Quantity { get; set; }
    }
}
