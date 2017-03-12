using FluentAssertions;
using Globalmantics.Domain;
using Globalmantics.Logic;
using Xunit;

namespace Globalmantics.IntegrationTests
{
    [Collection("Integration test collection")]
    public class UserServiceTests : IntegrationTestBase
    {
        [Fact]
        public void CanCreateUser()
        {
            using (var context = GivenGlobalmanticsContext())
            {
                var userSerice = new UserService(context);

                User user = userSerice.GetUserByEmail(
                    "test@globalmantics.com");
                context.SaveChanges();

                user.UserId.Should().NotBe(0);
                user.Email.Should().Be("test@globalmantics.com");
            }
        }
    }
}
