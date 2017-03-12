using Globalmantics.Domain;
using Highway.Data;
using System.Linq;

namespace Globalmantics.Logic.Queries
{
    public class CatalogItemBySku : Scalar<CatalogItem>
    {
        public CatalogItemBySku(string sku)
        {
            ContextQuery = c => c.AsQueryable<CatalogItem>()
                .FirstOrDefault(x => x.Sku == sku);
        }
    }
}
