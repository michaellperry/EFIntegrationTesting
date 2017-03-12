namespace Globalmantics.Domain
{
    public class CatalogItem
    {
        private CatalogItem()
        {
        }

        public int CatalogItemId { get; private set; }

        public string Sku { get; private set; }

        public string Description { get; private set; }

        public decimal UnitPrice { get; private set; }

        public static CatalogItem Create(string sku, string description, decimal unitPrice)
        {
            return new CatalogItem
            {
                Sku = sku,
                Description = description,
                UnitPrice = unitPrice
            };
        }
    }
}
