using FluentAssertions;
using Globalmantics.DAL;
using Globalmantics.Domain;
using Globalmantics.Logic;
using Highway.Data;
using NUnit.Framework;
using System;
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

        [Test]
        public void GroupItemsOfSameKind()
        {
            var context = new GlobalmanticsContext();
            var userService = new UserService(context);
            var cartService = new CartService(context);

            var user = GivenUser(context, userService);

            var cart = cartService.GetCartForUser(user);
            context.SaveChanges();

            cartService.AddItemToCart(cart, "CAFE-314", 2);
            cartService.AddItemToCart(cart, "CAFE-314", 1);
            context.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(3);
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
