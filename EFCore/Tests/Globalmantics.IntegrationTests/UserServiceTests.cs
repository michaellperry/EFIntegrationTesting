using FluentAssertions;
using Globalmantics.DAL;
using Globalmantics.DAL.Entities;
using Globalmantics.Logic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globalmantics.IntegrationTests
{
    [TestFixture]
    public class UserServiceTests : IntegrationTestBase
    {
        [Test]
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
