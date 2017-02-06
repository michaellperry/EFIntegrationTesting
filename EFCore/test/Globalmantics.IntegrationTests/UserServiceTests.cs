using FluentAssertions;
using Globalmantics.DAL;
using Globalmantics.DAL.Entities;
using Globalmantics.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Globalmantics.IntegrationTests
{
    [Collection("Integration test collection")]
    public class UserServiceTests : IntegrationTestBase
    {
        [Fact]
        public void CanCreateUser()
        {
            var context = GivenGlobalmanticsContext();
            var userSerice = new UserService(context);

            User user = userSerice.GetUserByEmail(
                "test@globalmantics.com");
            context.SaveChanges();

            user.UserId.Should().NotBe(0);
            user.Email.Should().Be("test@globalmantics.com");
        }
    }
}
