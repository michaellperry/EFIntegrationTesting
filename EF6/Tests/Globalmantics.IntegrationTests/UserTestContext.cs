using Globalmantics.Domain;
using Globalmantics.Logic;
using Highway.Data;
using System;

namespace Globalmantics.IntegrationTests
{
    public class UserTestContext : TestContext
    {
        public UserService UserService { get; set; }
        public string EmailAddress { get; } =
            $"test{Guid.NewGuid().ToString()}@globalmantics.com";

        protected UserTestContext()
        {
            UserService = new UserService(Repository, Log);
        }

        public User GivenUser()
        {
            var user = UserService.GetUserByEmail(EmailAddress);
            DataContext.Commit();
            return user;
        }

        public static UserTestContext GivenServices()
        {
            return new UserTestContext();
        }
    }
}
