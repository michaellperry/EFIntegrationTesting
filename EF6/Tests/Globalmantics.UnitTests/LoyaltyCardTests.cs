using FluentAssertions;
using NUnit.Framework;

namespace Globalmantics.UnitTests
{
    [TestFixture]
    public class LoyaltyCardTests
    {
        [Test]
        public void NoNotApplyDiscountToNonLoyaltyCardHolders()
        {
            var services = CartServiceContext.GivenServices();
            var cart = services.WhenLoadCart();

            services.WhenAddItemToCart(cart);

            cart.CartItems.Should().ContainSingle()
                .Which.ItemTotal.Should().Be(18.80m);
        }

        [Test]
        public void ApplyDiscountToLoyaltyCardHolders()
        {
            var services = CartServiceContext.GivenServices();
            services.GivenLoyaltyCard();
            var cart = services.WhenLoadCart();

            services.WhenAddItemToCart(cart);

            cart.CartItems.Should().ContainSingle()
                .Which.ItemTotal.Should().Be(0.9m * 18.80m);
        }
    }
}
