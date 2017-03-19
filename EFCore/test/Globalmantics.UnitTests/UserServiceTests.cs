using FluentAssertions;
using Globalmantics.DAL;
using Globalmantics.Domain;
using Globalmantics.Logic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Globalmantics.UnitTests
{
    public class UserServiceTests
    {
        [Fact]
        public void CanCreateUser()
        {
            var context = new GlobalmanticsContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase()
                .Options);
            var userService = new UserService(context);

            User user = userService.GetUserByEmail(
                "test@globalmantics.com");
            context.SaveChanges();

            user.UserId.Should().NotBe(0);
            user.Email.Should().Be("test@globalmantics.com");
        }
    }
}
