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
    public class CartServiceTests
    {
        [Fact]
        public void CanLoadCartWithNoItems()
        {
            string databaseName = Guid.NewGuid().ToString();

            var context = new GlobalmanticsContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options);
            var userService = new UserService(context);
            var cartService = new CartService(context);

            var user = userService.GetUserByEmail("test@globalmantics.com");
            context.SaveChanges();
            var cart = cartService.GetCartForUser(user);
            context.SaveChanges();

            cart.CartItems.Count().Should().Be(0);
        }

        [Fact]
        public void CanLoadCartWithOneItem()
        {
            string databaseName = Guid.NewGuid().ToString();

            InitializeCartWithOneItem(databaseName);

            var context = new GlobalmanticsContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName)
                .Options);
            var userService = new UserService(context);
            var cartService = new CartService(context);

            var user = userService.GetUserByEmail("test@globalmantics.com");
            context.SaveChanges();
            var cart = cartService.GetCartForUser(user);
            context.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(2);
        }

        private void InitializeCartWithOneItem(string databaseName)
        {
            var context = new GlobalmanticsContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName)
                .Options);

            var user = context.Add(User.Create("test@globalmantics.com"))
                .Entity;
            context.SaveChanges();
            var cart = context.Add(Cart.Create(user.UserId)).Entity;
            var catalogItem = context.Add(CatalogItem.Create
            (
                sku: "CAFE-314",
                description: "1 Pound Guatemalan Coffee Beans",
                unitPrice: 18.80m
            )).Entity;
            cart.AddItem(catalogItem, 2);
            context.SaveChanges();
        }
    }
}
