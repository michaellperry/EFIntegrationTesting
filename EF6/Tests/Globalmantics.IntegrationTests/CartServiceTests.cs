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

            cart.CartItems.Count().Should().Be(0);
        }
    }
}
