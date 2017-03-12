using Globalmantics.Domain;
using Highway.Data;
using System.Linq;

namespace Globalmantics.Logic.Queries
{
    public class UserByEmail : Scalar<User>
    {
        public UserByEmail(string emailAddress)
        {
            ContextQuery = c => c.AsQueryable<User>()
                .FirstOrDefault(x => x.Email == emailAddress);
        }
    }
}
