using FluentAssertions;
using Globalmantics.Logic;
using System.Linq;
using Xunit;

namespace Globalmantics.UnitTests
{
    public class CartServiceTests : UnitTestBase
    {
        [Fact]
        public void CanAddItemToCart()
        {
            using (var context = GivenGlobalmanticsContext())
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
