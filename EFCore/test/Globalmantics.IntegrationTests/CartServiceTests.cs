using FluentAssertions;
using Globalmantics.Domain;
using System.Linq;
using Xunit;

namespace Globalmantics.IntegrationTests
{
    [Collection("Integration test collection")]
    public class CartServiceTests : IntegrationTestBase
    {
        [Fact]
        public void CartIsInitiallyEmpty()
        {
            using (var services = CartServiceContext.GivenServices())
            {
                var cart = services.WhenLoadCart();

                cart.CartItems.Count().Should().Be(0);
            }
        }

        [Fact]
        public void CanAddItemToCart()
        {
            using (var services = CartServiceContext.GivenServices())
            {
                var cart = services.WhenLoadCart();

                services.WhenAddItemToCart(cart);

                cart.CartItems.Count().Should().Be(1);
            }
        }

        [Fact]
        public void GroupItemsOfSameKind()
        {
            using (var services = CartServiceContext.GivenServices())
            {
                var cart = services.WhenLoadCart();

                services.WhenAddItemToCart(cart, quantity: 2);
                services.WhenAddItemToCart(cart, quantity: 1);

                cart.CartItems.Count().Should().Be(1);
                cart.CartItems.Single().Quantity.Should().Be(3);
            }
        }

        [Fact]
        public void CanLoadCartWithOneItem()
        {
            using (var services = CartServiceContext.GivenServices())
            {
                InitializeCartWithOneItem(services.EmailAddress);

                var cart = services.WhenLoadCart();

                cart.CartItems.Count().Should().Be(1);
                cart.CartItems.Single().Quantity.Should().Be(2);
            }
        }

        private void InitializeCartWithOneItem(string emailAddress)
        {
            using (var context = GivenGlobalmanticsContext(beginTransaction: false))
            {
                var user = context.Add(User.Create(emailAddress)).Entity;
                context.SaveChanges();
                var cart = context.Add(Cart.Create(user.UserId)).Entity;
                var catalogItem = context.Set<CatalogItem>()
                    .Single(x => x.Sku == "CAFE-314");
                cart.AddItem(catalogItem, 2);
                context.SaveChanges();
            }
        }
    }
}
