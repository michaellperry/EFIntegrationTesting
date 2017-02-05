namespace Globalmantics.DAL.Entities
{
    public class CartLine
    {
        public int CartLineId { get; set; }

        public virtual Cart Cart { get; set; }
        public int CartId { get; set; }

        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
    }
}
