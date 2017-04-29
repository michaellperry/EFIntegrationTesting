using FluentAssertions;
using Globalmantics.Domain;
using NUnit.Framework;

namespace Globalmantics.IntegrationTests
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void CanCreateUser()
        {
            var services = UserServiceContext.GivenServices();

            User user = services.WhenCreateUser();

            user.UserId.Should().NotBe(0);
            user.Email.Should().Be(services.EmailAddress);
        }
    }
}
