using FluentAssertions;
using Globalmantics.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globalmantics.UnitTests
{
    [TestFixture]
    public class CartTests
    {
        [Test]
        public void CartIsInitiallyEmpty()
        {
            var cart = new Cart();

            cart.CartItems.Count.Should().Be(0);
        }

        [Test]
        public void CanAddItemToCart()
        {
            var cart = new Cart();

            cart.AddItem(new CatalogItem() { Sku = "CAFE-314" }, 2);

            cart.CartItems.Count.Should().Be(1);
        }
    }
}
