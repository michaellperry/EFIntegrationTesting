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
    public class LoyaltyCardTests
    {
        [Test]
        public void NoNotApplyDiscountToNonLoyaltyCardHolders()
        {
            CartServiceContext services = CartServiceContext.GivenServices();
            Cart cart = services.WhenLoadCart();
            services.WhenAddItemToCart(cart);
            cart.CartItems.Should().ContainSingle()
                .Which.LineTotal.Should().Be(18.80m);
        }

        [Test]
        public void ApplyDiscountToLoyaltyCardHolders()
        {
            CartServiceContext services = CartServiceContext.GivenServices();
            services.GivenLoyaltyCard();
            Cart cart = services.WhenLoadCart();
            services.WhenAddItemToCart(cart);
            cart.CartItems.Should().ContainSingle()
                .Which.LineTotal.Should().Be(0.9m * 18.80m);
        }
    }
}
