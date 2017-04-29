using Globalmantics.Domain;
using Highway.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globalmantics.Logic.Queries
{
    public class UserByEmail : Scalar<User>
    {
        public UserByEmail(string emailAddress)
        {
            ContextQuery = context => context.AsQueryable<User>()
                .Include(x => x.LoyaltyCards)
                .FirstOrDefault(x => x.Email == emailAddress);
        }
    }
}
