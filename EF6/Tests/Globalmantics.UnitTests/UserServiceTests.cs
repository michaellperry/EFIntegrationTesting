using FluentAssertions;
using Globalmantics.Domain;
using Highway.Data;
using Highway.Data.Contexts;
using NUnit.Framework;

namespace Globalmantics.UnitTests
{
    [TestFixture]
    public class UserServiceTests : UnitTests
    {
        [Test]
        public void CanCreateUser()
        {
            var context = new InMemoryDataContext();
            var userService = GivenUserService(new Repository(context));

            User user = userService.GetUserByEmail(
                "test@globalmantics.com");
            context.Commit();

            user.UserId.Should().NotBe(0);
            user.Email.Should().Be("test@globalmantics.com");
        }
    }
}
