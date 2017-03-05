using FluentAssertions;
using Globalmantics.DAL.Entities;
using Xunit;

namespace Globalmantics.IntegrationTests
{
    [Collection("Integration test collection")]
    public class IdentityProviderExploration : IntegrationTestBase
    {
        [Fact]
        public void IdentityProviderAllocatesIds()
        {
            using (var context = GivenGlobalmanticsContext())
            {
                var user = new User
                {
                    Email = "test@globalmantics.com"
                };
                user.UserId.Should().Be(0);

                context.User.Add(user);
                user.UserId.Should().NotBe(0);

                context.SaveChanges();
                user.UserId.Should().NotBe(0);
            }
        }
    }
}
