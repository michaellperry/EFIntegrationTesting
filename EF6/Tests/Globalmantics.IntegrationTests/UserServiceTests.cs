using FluentAssertions;
using Globalmantics.DAL;
using Globalmantics.DAL.Entities;
using Globalmantics.Logic;
using NUnit.Framework;

namespace Globalmantics.IntegrationTests
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void CanCreateUser()
        {
            var context = new GlobalmanticsContext();
            var userSerice = new UserService(context);

            User user = userSerice.GetUserByEmail(
                "test@globalmantics.com");
            context.SaveChanges();

            user.UserId.Should().NotBe(0);
            user.Email.Should().Be("test@globalmantics.com");
        }
    }
}
