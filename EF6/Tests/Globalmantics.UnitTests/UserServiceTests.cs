using FluentAssertions;
using Globalmantics.Domain;
using Globalmantics.Logic;
using Highway.Data;
using Highway.Data.Contexts;
using NUnit.Framework;

namespace Globalmantics.UnitTests
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void CanCreateUser()
        {
            var context = new InMemoryDataContext();
            var userService = new UserService(new Repository(context));

            User user = userService.GetUserByEmail(
                "test@globalmantics.com");
            context.Commit();

            user.UserId.Should().NotBe(0);
            user.Email.Should().Be("test@globalmantics.com");
        }
    }
}
