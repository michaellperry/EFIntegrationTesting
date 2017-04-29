using Globalmantics.Domain;
using Highway.Data;
using System.Data.Entity;
using System.Linq;

namespace Globalmantics.Logic.Queries
{
    public class UserByEmail : Scalar<User>
    {
        public UserByEmail(string emailAddress)
        {
            ContextQuery = context => context.AsQueryable<User>()
                .FirstOrDefault(x => x.Email == emailAddress);
        }
    }
}
