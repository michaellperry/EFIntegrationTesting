using System;
using Globalmantics.DAL;
using Globalmantics.DAL.Entities;
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

        public User GetUserByEmail(string email)
        {
            var user = _context.Users
                .Where(x => x.Email == email)
                .SingleOrDefault();

            if (user == null)
            {
                user = _context.Users.Add(new User
                {
                    Email = email
                });
            }

            return user;
        }
    }
}