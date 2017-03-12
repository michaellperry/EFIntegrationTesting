using Globalmantics.Domain;
using Highway.Data;
using System.Data.Entity;
using System.Linq;

namespace Globalmantics.Logic.Queries
{
    public class CartForUser : Scalar<Cart>
    {
        public CartForUser(int userId)
        {
            ContextQuery = c => c.AsQueryable<Cart>()
                .Include(x => x.CartItems)
                .FirstOrDefault(x => x.UserId == userId);
        }
    }
}
