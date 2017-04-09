using Globalmantics.Domain;
using Globalmantics.Logic;
using System;

namespace Globalmantics.IntegrationTests
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

        public User WhenGetUserByEmail()
        {
            User user = UserService.GetUserByEmail(EmailAddress);
            Context.SaveChanges();
            return user;
        }

        public static UserServiceContext GivenServices()
        {
            return new UserServiceContext();
        }
    }
}
