using Globalmantics.DAL;
using Globalmantics.Domain;
using Globalmantics.Logic.Queries;
using Highway.Data;
using System.Linq;

namespace Globalmantics.Logic
{
    public class UserService
    {
        private GlobalmanticsContext _context;

        public UserService(GlobalmanticsContext context)
        {
            _context = context;
        }

        public User GetUserByEmail(string emailAddress)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Email == emailAddress);

            if (user == null)
            {
                user = _context.Users.Add(User.Create(emailAddress));
            }

            return user;
        }
    }
}