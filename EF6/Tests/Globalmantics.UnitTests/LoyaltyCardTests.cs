using FluentAssertions;
using NUnit.Framework;

namespace Globalmantics.UnitTests
{
    [TestFixture]
    public class LoyaltyCardTests
    {
        [Test]
        public void DoNotApplyDiscountToNonLoyaltyCardHolders()
        {
            var services = CartServiceContext.GivenServices();
            var cart = services.WhenLoadCart();

            services.WhenAddItemToCart(cart);

            cart.CartItems.Should().ContainSingle()
                .Which.ItemTotal.Should().Be(18.80m);
        }
    }
}
