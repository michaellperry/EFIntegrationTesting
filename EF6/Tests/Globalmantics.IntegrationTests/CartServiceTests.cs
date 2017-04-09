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
            var services = GivenServices();

            var cart = WhenLoadCart(services);

            cart.CartItems.Count().Should().Be(0);
        }

        [Test]
        public void CanAddItemToCart()
        {
            var services = GivenServices();

            var cart = WhenLoadCart(services);

            services.CartService.AddItemToCart(cart, "CAFE-314", 2);
            services.Context.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
        }

        [Test]
        public void GroupItemsOfSameKind()
        {
            var services = GivenServices();

            Cart cart = WhenLoadCart(services);

            services.CartService.AddItemToCart(cart, "CAFE-314", 2);
            services.CartService.AddItemToCart(cart, "CAFE-314", 1);
            services.Context.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(3);
        }

        [Test]
        public void CanLoadCartWithOneItem()
        {
            var services = GivenServices();
            InitializeCartWithOneItem(services.EmailAddress);

            var cart = WhenLoadCart(services);

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(2);
        }

        private void InitializeCartWithOneItem(string emailAddress)
        {
            var configuration = new GlobalmanticsMappingConfiguration();
            var context = new DataContext("GlobalmanticsContext", configuration);
            var user = context.Add(User.Create(emailAddress));
            context.Commit();
            var cart = context.Add(Cart.Create(user.UserId));
            var catalogItem = context.AsQueryable<CatalogItem>()
                .Single(x => x.Sku == "CAFE-314");
            cart.AddItem(catalogItem, 2);
            context.Commit();
        }

        private static CartServiceContext GivenServices()
        {
            var configuration = new GlobalmanticsMappingConfiguration();
            var context = new DataContext("GlobalmanticsContext", configuration);
            var repository = new Repository(context);
            var userService = new UserService(repository);
            var cartService = new CartService(repository, new MockLog());

            var services = new CartServiceContext
            {
                Context = context,
                UserService = userService,
                CartService = cartService
            };
            return services;
        }

        private static User GivenUser(IUnitOfWork context, UserService userService, string emailAddress)
        {
            var user = userService.GetUserByEmail(
                emailAddress);
            context.Commit();
            return user;
        }

        private static Cart WhenLoadCart(CartServiceContext services)
        {
            var user = GivenUser(services.Context, services.UserService,
                services.EmailAddress);

            var cart = services.CartService.GetCartForUser(user);
            services.Context.SaveChanges();
            return cart;
        }
    }
}
