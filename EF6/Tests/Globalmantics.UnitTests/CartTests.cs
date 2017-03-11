using FluentAssertions;
using Globalmantics.Domain;
using NUnit.Framework;
using System.Linq;

namespace Globalmantics.UnitTests
{
    [TestFixture]
    public class CartTests
    {
        [Test]
        public void CartIsInitiallyEmpty()
        {
            var cart = Cart.Create(0);

            cart.CartItems.Count.Should().Be(0);
        }

        [Test]
        public void CanAddItemToCart()
        {
            var cart = Cart.Create(0);
            var catalogItem = new CatalogItem() { Sku = "CAFE-314" };

            cart.AddItem(catalogItem, 2);

            cart.CartItems.Count.Should().Be(1);
        }

        [Test]
        public void GroupsItemsOfSameKind()
        {
            var cart = Cart.Create(0);
            var catalogItem = new CatalogItem() { Sku = "CAFE-314" };

            cart.AddItem(catalogItem, 2);
            cart.AddItem(catalogItem, 1);

            cart.CartItems.Count.Should().Be(1);
            cart.CartItems.Single().Quantity.Should().Be(3);
        }
    }
}
