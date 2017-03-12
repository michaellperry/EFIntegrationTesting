using FluentAssertions;
using Globalmantics.Domain;
using Globalmantics.Logic;
using Highway.Data;
using Highway.Data.Contexts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globalmantics.UnitTests
{
    [TestFixture]
    public class CartServiceTests
    {
        [Test]
        public void CanGetCartWithNoItems()
        {
            var context = new InMemoryDataContext();
            var repository = new Repository(context);
            var userService = new UserService(repository);
            var cartService = new CartService(repository);

            var user = userService.GetUserByEmail("test@globalmantics.com");
            context.Commit();
            var cart = cartService.GetCartForUser(user);
            context.Commit();

            cart.CartItems.Count().Should().Be(0);
        }

        [Test]
        public void DifferentUsersHaveDifferentCarts()
        {
            var context = new InMemoryDataContext();
            var repository = new Repository(context);
            var userService = new UserService(repository);
            var cartService = new CartService(repository);

            var user1 = userService.GetUserByEmail("test1@globalmantics.com");
            context.Commit();
            var cart1 = cartService.GetCartForUser(user1);
            context.Commit();

            var user2 = userService.GetUserByEmail("test2@globalmantics.com");
            context.Commit();
            var cart2 = cartService.GetCartForUser(user2);
            context.Commit();

            cart1.Should().NotBeSameAs(cart2);
        }

        [Test]
        public void CanGetCartWithOneItem()
        {
            var context = new InMemoryDataContext();
            var initialUser = context.Add(User.Create("test@globalmantics.com"));
            context.Commit();
            var initialCart = context.Add(Cart.Create(initialUser.UserId));
            var catalogItem = context.Add(CatalogItem.Create
            (
                sku: "CAFE-314",
                description: "1 Pound Guatemalan Coffee Beans",
                unitPrice: 18.80m
            ));
            initialCart.AddItem(catalogItem, 2);
            context.Commit();

            var repository = new Repository(context);
            var userService = new UserService(repository);
            var cartService = new CartService(repository);

            var user = userService.GetUserByEmail("test@globalmantics.com");
            context.Commit();
            var cart = cartService.GetCartForUser(user);
            context.Commit();

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(2);
        }
    }
}
