using Globalmantics.DAL;
using Globalmantics.Domain;
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
            var user = _context.User
                .FirstOrDefault(x => x.Email == emailAddress);

            if (user == null)
            {
                user = _context.User.Add(User.Create(emailAddress)).Entity;
            }

            return user;
        }
    }
}