using Globalmantics.Domain;
using Globalmantics.Logic.Queries;
using Highway.Data;

namespace Globalmantics.Logic
{
    public class UserService
    {
        private IRepository _repository;

        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public User GetUserByEmail(string emailAddress)
        {
            var user = _repository.Find(new UserByEmail(emailAddress));

            if (user == null)
            {
                user = _repository.Context.Add(User.Create(emailAddress));
            }

            return user;
        }
    }
}