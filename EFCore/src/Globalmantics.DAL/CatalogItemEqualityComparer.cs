using Globalmantics.Domain;
using System.Collections.Generic;

namespace Globalmantics.DAL
{
    public class CatalogItemEqualityComparer : IEqualityComparer<CatalogItem>
    {
        public bool Equals(CatalogItem x, CatalogItem y)
        {
            return
                x.Description == y.Description &&
                x.Sku == y.Sku &&
                x.UnitPrice == y.UnitPrice;
        }

        public int GetHashCode(CatalogItem obj)
        {
            return ((
                obj.Description.GetHashCode()) * 37 +
                obj.Sku.GetHashCode()) * 37 +
                obj.UnitPrice.GetHashCode();

        }
    }
}