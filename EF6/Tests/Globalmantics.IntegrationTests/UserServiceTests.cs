using FluentAssertions;
using Globalmantics.DAL;
using Globalmantics.Domain;
using Globalmantics.Logic;
using Highway.Data;
using NUnit.Framework;

namespace Globalmantics.IntegrationTests
{
    [TestFixture]
    public class UserServiceTests : IntegrationTests
    {
        [Test]
        public void CanCreateUser()
        {
            var configuration = new GlobalmanticsMappingConfiguration();
            var context = new DataContext("GlobalmanticsContext", configuration);
            var repository = new Repository(context);
            var userService = GivenUserService(repository);

            User user = userService.GetUserByEmail(
                "test@globalmantics.com");
            context.SaveChanges();

            user.UserId.Should().NotBe(0);
            user.Email.Should().Be("test@globalmantics.com");
        }
    }
}
