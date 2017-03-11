using FluentAssertions;
using Globalmantics.DAL;
using Globalmantics.Logic;
using NUnit.Framework;
using System.Linq;
using Globalmantics.Domain;
using System;

namespace Globalmantics.IntegrationTests
{
    [TestFixture]
    public class CartServiceTests
    {
        [Test]
        public void CartIsInitiallyEmpty()
        {
            var context = new GlobalmanticsContext();
            var userService = new UserService(context);
            var cartService = new CartService(context);

            var user = GivenUser(context, userService);

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

            var user = GivenUser(context, userService);

            var cart = cartService.GetCartForUser(user);
            context.SaveChanges();

            cartService.AddItemToCart(cart, "CAFE-314", 2);
            context.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
        }

        private static User GivenUser(GlobalmanticsContext context, UserService userService)
        {
            var user = userService.GetUserByEmail(
                $"test{Guid.NewGuid().ToString()}@globalmantics.com");
            context.SaveChanges();
            return user;
        }
    }
}
