using FluentAssertions;
using Globalmantics.Domain;
using NUnit.Framework;

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

            cart.AddItem(new CatalogItem() { Sku = "CAFE-314" }, 2);

            cart.CartItems.Count.Should().Be(1);
        }
    }
}
