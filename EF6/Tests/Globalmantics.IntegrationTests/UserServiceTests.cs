using FluentAssertions;
using NUnit.Framework;

namespace Globalmantics.IntegrationTests
{
    [TestFixture]
    public class UserServiceTests : IntegrationTests
    {
        [Test]
        public void CanCreateUser()
        {
            var services = UserTestContext.GivenServices();

            var user = services.WhenGetUserByEmail();

            user.UserId.Should().NotBe(0);
            user.Email.Should().Be(services.EmailAddress);
        }
    }
}
