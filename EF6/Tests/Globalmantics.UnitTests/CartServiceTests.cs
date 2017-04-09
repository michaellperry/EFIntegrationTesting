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
        public void CanLoadCartWithNoItems()
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
        public void CanLoadCartWithOneItem()
        {
            var context = new InMemoryDataContext();
            InitializeCartWithOneItem(context);

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

        private void InitializeCartWithOneItem(InMemoryDataContext context)
        {
            var user = context.Add(User.Create("test@globalmantics.com"));
            context.Commit();
            var cart = context.Add(Cart.Create(user.UserId));
            var catalogItem = context.Add(CatalogItem.Create
            (
                sku: "CAFE-314",
                description: "1 Pound Guatemalan Coffee Beans",
                unitPrice: 18.80m
            ));
            cart.AddItem(catalogItem, 2);
            context.Commit();
        }
    }
}
