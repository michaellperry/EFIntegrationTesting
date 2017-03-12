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
    [TestFixture]
    public class CartServiceTests
    {
        [Test]
        public void CartIsInitiallyEmpty()
        {
            var configuration = new GlobalmanticsMappingConfiguration();
            var context = new DataContext("GlobalmanticsContext", configuration);
            var repository = new Repository(context);
            var userService = new UserService(repository);
            var cartService = new CartService(repository);

            var user = GivenUser(context, userService);

            var cart = cartService.GetCartForUser(user);
            context.SaveChanges();

            cart.CartItems.Count().Should().Be(0);
        }

        [Test]
        public void CanLoadCartWithOneItem()
        {
            var configuration = new GlobalmanticsMappingConfiguration();
            var context = new DataContext("GlobalmanticsContext", configuration);

            var initialUser = context.Add(User.Create("test@globalmantics.com"));
            context.Commit();
            var initialCart = context.Add(Cart.Create(initialUser.UserId));
            var catalogItem = context.AsQueryable<CatalogItem>()
                .Single(x => x.Sku == "CAFE-314");
            initialCart.AddItem(catalogItem, 2);
            context.Commit();

            var repository = new Repository(context);
            var userService = new UserService(repository);
            var cartService = new CartService(repository);

            var user = GivenUser(context, userService);

            var cart = cartService.GetCartForUser(user);
            context.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
        }

        [Test]
        public void CanAddItemToCart()
        {
            var configuration = new GlobalmanticsMappingConfiguration();
            var context = new DataContext("GlobalmanticsContext", configuration);
            var repository = new Repository(context);
            var userService = new UserService(repository);
            var cartService = new CartService(repository);

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
            var configuration = new GlobalmanticsMappingConfiguration();
            var context = new DataContext("GlobalmanticsContext", configuration);
            var repository = new Repository(context);
            var userService = new UserService(repository);
            var cartService = new CartService(repository);

            var user = GivenUser(context, userService);

            var cart = cartService.GetCartForUser(user);
            context.SaveChanges();

            cartService.AddItemToCart(cart, "CAFE-314", 2);
            cartService.AddItemToCart(cart, "CAFE-314", 1);
            context.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(3);
        }

        private static User GivenUser(IUnitOfWork context, UserService userService)
        {
            var user = userService.GetUserByEmail(
                $"test{Guid.NewGuid().ToString()}@globalmantics.com");
            context.Commit();
            return user;
        }
    }
}
