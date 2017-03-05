using FluentAssertions;
using Globalmantics.DAL;
using Globalmantics.Logic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace Globalmantics.UnitTests
{
    public class CartServiceTests
    {
        [Fact]
        public void CanAddItemToCart()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase()
                .Options;
            using (var context = new GlobalmanticsContext(options))
            {
                var userService = new UserService(context);
                var cartService = new CartService(context);

                var user = userService.GetUserByEmail("test@globalmantics.com");
                context.SaveChanges();

                var cart = cartService.GetCartForUser(user);
                context.SaveChanges();

                cart.CartItems.Count().Should().Be(0);
            }
        }
    }
}
