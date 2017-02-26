using FluentAssertions;
using Globalmantics.DAL;
using Globalmantics.Logic;
using NUnit.Framework;
using System.Linq;

namespace Globalmantics.IntegrationTests
{
    [Isolated]
    [TestFixture]
    public class CartServiceTests
    {
        [Test]
        public void CartIsInitiallyEmpty()
        {
            var context = new GlobalmanticsContext();
            var userService = new UserService(context);
            var cartService = new CartService(context);

            var user = userService.GetUserByEmail("test@globalmantics.com");
            context.SaveChanges();

            var cart = cartService.GetCartForUser(user);
            context.SaveChanges();

            cart.CartItems.Count().Should().Be(0);
        }

        [Test]
        public void CanAddItemToCart()
        {
            var context = new GlobalmanticsContext();
            var userService = new UserService(context);
            var cartService = new CartService(context);

            var user = userService.GetUserByEmail("test@globalmantics.com");
            context.SaveChanges();

            var cart = cartService.GetCartForUser(user);
            cartService.AddItemToCart(cart, "CAFE-314", 2);
            context.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
        }
    }
}
