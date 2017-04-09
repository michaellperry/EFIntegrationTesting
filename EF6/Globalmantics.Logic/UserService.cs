using Globalmantics.Domain;
using Globalmantics.Logic.Queries;
using Highway.Data;

namespace Globalmantics.Logic
{
    public class UserService
    {
        private readonly IRepository _repository;
        private readonly ILog _log;

        public UserService(IRepository repository, ILog log)
        {
            _repository = repository;
            _log = log;
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