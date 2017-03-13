using Globalmantics.Domain;
using Highway.Data;
using System;
using System.Collections.Generic;
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
                .FirstOrDefault(x => x.Email == emailAddress);
        }
    }
}
