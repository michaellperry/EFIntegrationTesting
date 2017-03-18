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
            var context = new GlobalmanticsContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase()
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
            var context = new GlobalmanticsContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase()
                .Options);
            InitializeCartWithOneItem();

            var userService = new UserService(context);
            var cartService = new CartService(context);

            var user = userService.GetUserByEmail("test@globalmantics.com");
            context.SaveChanges();
            var cart = cartService.GetCartForUser(user);
            context.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(2);
        }

        private void InitializeCartWithOneItem()
        {
            var context = new GlobalmanticsContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase()
                .Options);

            var user = context.Add(User.Create("test@globalmantics.com")).Entity;
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
