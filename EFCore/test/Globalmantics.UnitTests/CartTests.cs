using FluentAssertions;
using Globalmantics.Domain;
using Xunit;
using System.Linq;

namespace Globalmantics.UnitTests
{
    public class CartTests
    {
        [Fact]
        public void CartIsInitiallyEmpty()
        {
            var cart = Cart.Create(0);

            cart.CartItems.Count().Should().Be(0);
        }

        [Fact]
        public void CanAddItemToCart()
        {
            var cart = Cart.Create(0);
            var catalogItem = CatalogItem.Create(
                sku: "CAFE-314",
                description: "1 Pound Guatemalan Coffee Beans",
                unitPrice: 18.80m);

            cart.AddItem(catalogItem, 2);

            cart.CartItems.Count().Should().Be(1);
        }

        [Fact]
        public void GroupItemsOfSameKind()
        {
            var cart = Cart.Create(0);
            var catalogItem = CatalogItem.Create(
                sku: "CAFE-314",
                description: "1 Pound Guatemalan Coffee Beans",
                unitPrice: 18.80m);

            cart.AddItem(catalogItem, 2);
            cart.AddItem(catalogItem, 1);

            cart.CartItems.Count().Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(3);
        }
    }
}
