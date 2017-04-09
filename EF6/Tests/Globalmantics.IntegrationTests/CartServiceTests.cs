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

            var cart = WhenLoadCart(services);

            cart.CartItems.Count().Should().Be(0);
        }

        [Test]
        public void CanAddItemToCart()
        {
            var services = GivenServices();

            var cart = WhenLoadCart(services);

            services.CartService.AddItemToCart(cart, "CAFE-314", 2);
            services.DataContext.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
        }

        [Test]
        public void GroupItemsOfSameKind()
        {
            var services = GivenServices();

            var cart = WhenLoadCart(services);

            services.CartService.AddItemToCart(cart, "CAFE-314", 2);
            services.CartService.AddItemToCart(cart, "CAFE-314", 1);
            services.DataContext.SaveChanges();

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(3);
        }

        [Test]
        public void CanLoadCartWithOneItem()
        {
            var emailAddress = GivenEmailAddress();
            InitializeCartWithOneItem(emailAddress);
            var services = GivenServices();

            var cart = WhenLoadCart(services, emailAddress);

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

        private static User GivenUser(ServiceContext services, string emailAddress)
        {
            var user = services.UserService.GetUserByEmail(emailAddress);
            services.DataContext.Commit();
            return user;
        }

        private static string GivenEmailAddress()
        {
            return $"test{Guid.NewGuid().ToString()}@globalmantics.com";
        }

        private static CartService GivenCartService(IRepository repository)
        {
            return new CartService(repository, new MockLog());
        }

        private static Cart WhenLoadCart(ServiceContext services)
        {
            return WhenLoadCart(services, GivenEmailAddress());
        }

        private static Cart WhenLoadCart(ServiceContext services, string emailAddress)
        {
            var user = GivenUser(services, emailAddress);

            var cart = services.CartService.GetCartForUser(user);
            services.DataContext.SaveChanges();
            return cart;
        }
    }
}
