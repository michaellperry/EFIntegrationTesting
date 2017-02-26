using FluentAssertions;
using Globalmantics.Logic;
using System.Linq;
using Xunit;

namespace Globalmantics.IntegrationTests
{
    [Collection("Integration test collection")]
    public class CartServiceTests : IntegrationTestBase
    {
        [Fact]
        public void CartIsInitiallyEmpty()
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

                cartService.AddItemToCart(cart, "CAFE-314", 2);
                context.SaveChanges();

                cart.CartItems.Count().Should().Be(1);
            }
        }
    }
}
