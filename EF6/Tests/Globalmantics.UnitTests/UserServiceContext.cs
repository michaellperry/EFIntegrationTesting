using Globalmantics.Domain;
using Globalmantics.Logic;
using System;

namespace Globalmantics.UnitTests
{
    public class UserServiceContext : TestContext
    {
        public UserService UserService { get; }
        public string EmailAddress { get; } =
            $"test{Guid.NewGuid().ToString()}@globalmantics.com";

        protected UserServiceContext()
        {
            UserService = new UserService(Repository);
        }

        public User WhenCreateUser()
        {
            User user = UserService.CreateUser(EmailAddress);
            Context.Commit();
            return user;
        }

        public static UserServiceContext GivenServices()
        {
            return new UserServiceContext();
        }
    }
}
