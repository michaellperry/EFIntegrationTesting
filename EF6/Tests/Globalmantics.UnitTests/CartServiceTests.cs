using FluentAssertions;
using Globalmantics.Domain;
using Globalmantics.Logic;
using Highway.Data;
using Highway.Data.Contexts;
using NUnit.Framework;
using System.Linq;

namespace Globalmantics.UnitTests
{
    [TestFixture]
    public class CartServiceTests : UnitTests
    {
        [Test]
        public void CanLoadCartWithNoItems()
        {
            var services = GivenServices();

            var cart = WhenLoadCart(services);

            cart.CartItems.Count().Should().Be(0);
        }

        [Test]
        public void CanLoadCartWithOneItem()
        {
            var services = GivenServices();
            InitializeCartWithOneItem(services.Context);

            var cart = WhenLoadCart(services);

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(2);
        }

        private static ServiceContext GivenServices()
        {
            var context = new InMemoryDataContext();
            var repository = new Repository(context);
            var userService = GivenUserService(repository);
            var cartService = GivenCartService(repository);

            return new ServiceContext
            {
                Context = context,
                UserService = userService,
                CartService = cartService
            };
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

        private static CartService GivenCartService(IRepository repository)
        {
            return new CartService(repository, new MockLog());
        }

        private static Cart WhenLoadCart(ServiceContext services)
        {
            var user = services.UserService.GetUserByEmail("test@globalmantics.com");
            services.Context.Commit();
            var cart = services.CartService.GetCartForUser(user);
            services.Context.Commit();
            return cart;
        }
    }
}
