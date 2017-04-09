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

            var user = GivenUser(services.Context, services.UserService);

            var cart = services.CartService.GetCartForUser(user);
            services.Context.SaveChanges();

            cart.CartItems.Count().Should().Be(0);
        }

        [Test]
        public void CanAddItemToCart()
        {
            var services = GivenServices();

            var user = GivenUser(services.Context, services.UserService);

            var cart = services.CartService.GetCartForUser(user);
            services.Context.SaveChanges();

            services.CartService.AddItemToCart(cart, "CAFE-314", 2);
            services.Context.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
        }

        [Test]
        public void GroupItemsOfSameKind()
        {
            var services = GivenServices();

            var user = GivenUser(services.Context, services.UserService);

            var cart = services.CartService.GetCartForUser(user);
            services.Context.SaveChanges();

            services.CartService.AddItemToCart(cart, "CAFE-314", 2);
            services.CartService.AddItemToCart(cart, "CAFE-314", 1);
            services.Context.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(3);
        }

        [Test]
        public void CanLoadCartWithOneItem()
        {
            InitializeCartWithOneItem();
            var services = GivenServices();

            var user = services.UserService.GetUserByEmail("test@globalmantics.com");
            services.Context.Commit();
            var cart = services.CartService.GetCartForUser(user);
            services.Context.Commit();

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(2);
        }

        private void InitializeCartWithOneItem()
        {
            var configuration = new GlobalmanticsMappingConfiguration();
            var context = new DataContext("GlobalmanticsContext", configuration);
            var user = context.Add(User.Create("test@globalmantics.com"));
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
            var cartService = GivenCartService(repository);

            var services = new CartServiceContext
            {
                Context = context,
                UserService = userService,
                CartService = cartService
            };
            return services;
        }

        private static CartService GivenCartService(Repository repository)
        {
            return new CartService(repository, new MockLog());
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
