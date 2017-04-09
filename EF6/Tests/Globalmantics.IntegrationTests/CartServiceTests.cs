using FluentAssertions;
using Globalmantics.DAL;
using Globalmantics.Domain;
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
            var services = CartTestContext.GivenServices();

            var cart = services.WhenLoadCart();

            cart.CartItems.Count().Should().Be(0);
        }

        [Test]
        public void CanAddItemToCart()
        {
            var services = CartTestContext.GivenServices();

            var cart = services.WhenLoadCart();

            services.WhenAddItemToCart(cart);

            cart.CartItems.Count().Should().Be(1);
        }

        [Test]
        public void GroupItemsOfSameKind()
        {
            var services = CartTestContext.GivenServices();

            var cart = services.WhenLoadCart();

            services.WhenAddItemToCart(cart, quantity: 2);
            services.WhenAddItemToCart(cart, quantity: 1);

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(3);
        }

        [Test]
        public void CanLoadCartWithOneItem()
        {
            var services = CartTestContext.GivenServices();
            InitializeCartWithOneItem(services.EmailAddress);

            var cart = services.WhenLoadCart();

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(2);
        }

        [Test]
        public void FailsToAddItemNotInCatalog()
        {
            var services = CartTestContext.GivenServices();
            var cart = services.WhenLoadCart();
            Action add = () => services.WhenAddItemToCart(cart, sku: "NOT-THERE");
            add.ShouldThrow<ArgumentException>("Item not found in catalog");
            cart.CartItems.Count.Should().Be(0);
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
    }
}
