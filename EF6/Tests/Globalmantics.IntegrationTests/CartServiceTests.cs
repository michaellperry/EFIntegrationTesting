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
    public class CartServiceTests : IntegrationTests
    {
        [Test]
        public void CartIsInitiallyEmpty()
        {
            var services = GivenServices();

            var user = GivenUser(services);

            var cart = services.CartService.GetCartForUser(user);
            services.DataContext.SaveChanges();

            cart.CartItems.Count().Should().Be(0);
        }

        [Test]
        public void CanAddItemToCart()
        {
            var services = GivenServices();

            var user = GivenUser(services);

            var cart = services.CartService.GetCartForUser(user);
            services.DataContext.SaveChanges();

            services.CartService.AddItemToCart(cart, "CAFE-314", 2);
            services.DataContext.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
        }

        [Test]
        public void GroupItemsOfSameKind()
        {
            var services = GivenServices();

            var user = GivenUser(services);

            var cart = services.CartService.GetCartForUser(user);
            services.DataContext.SaveChanges();

            services.CartService.AddItemToCart(cart, "CAFE-314", 2);
            services.CartService.AddItemToCart(cart, "CAFE-314", 1);
            services.DataContext.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(3);
        }

        [Test]
        public void CanLoadCartWithOneItem()
        {
            InitializeCartWithOneItem();
            var services = GivenServices();

            var user = services.UserService.GetUserByEmail("test@globalmantics.com");
            services.DataContext.Commit();
            var cart = services.CartService.GetCartForUser(user);
            services.DataContext.Commit();

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

        private static ServiceContext GivenServices()
        {
            var configuration = new GlobalmanticsMappingConfiguration();
            var context = new DataContext("GlobalmanticsContext", configuration);
            var repository = new Repository(context);
            var userService = GivenUserService(repository);
            var cartService = GivenCartService(repository);

            return new ServiceContext
            {
                DataContext = context,
                UserService = userService,
                CartService = cartService
            };
        }

        private static User GivenUser(ServiceContext services)
        {
            var user = services.UserService.GetUserByEmail(
                $"test{Guid.NewGuid().ToString()}@globalmantics.com");
            services.DataContext.Commit();
            return user;
        }

        private static CartService GivenCartService(IRepository repository)
        {
            return new CartService(repository, new MockLog());
        }
    }
}
