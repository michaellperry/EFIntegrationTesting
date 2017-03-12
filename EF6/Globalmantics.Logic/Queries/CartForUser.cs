using Globalmantics.Domain;
using Highway.Data;
using System.Linq;

namespace Globalmantics.Logic.Queries
{
    public class CartForUser : Scalar<Cart>
    {
        public CartForUser(int userId)
        {
            ContextQuery = c => c.AsQueryable<Cart>()
                .FirstOrDefault(x => x.UserId == userId);
        }
    }
}
