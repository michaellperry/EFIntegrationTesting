using FluentAssertions;
using Globalmantics.DAL;
using Globalmantics.Domain;
using Highway.Data;
using NUnit.Framework;

namespace Globalmantics.IntegrationTests
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

        [Test]
        public void ApplyDiscountToLoyaltyCardHolders()
        {
            var services = CartServiceContext.GivenServices();
            InitializeUserWithLoyaltyCard(services.EmailAddress);
            var cart = services.WhenLoadCart();

            services.WhenAddItemToCart(cart);

            cart.CartItems.Should().ContainSingle()
                .Which.ItemTotal.Should().Be(0.9m * 18.80m);
        }

        void InitializeUserWithLoyaltyCard(string emailAddress)
        {
            var configuration = new GlobalmanticsMappingConfiguration();
            var context = new DataContext("GlobalmanticsContext", configuration);
            var user = context.Add(User.Create(emailAddress));
            context.Commit();
            context.Add(LoyaltyCard.Create(user, "35196429541"));
            context.Commit();
        }
    }
}
