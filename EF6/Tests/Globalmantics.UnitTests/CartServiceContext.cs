using Globalmantics.Domain;
using Globalmantics.Logic;
using Highway.Data;
using Highway.Data.Contexts;
using System;

namespace Globalmantics.UnitTests
{
    public class CartServiceContext : UserServiceContext
    {
        public CartService CartService { get; }
        public User User { get; }

        protected CartServiceContext()
        {
            Context.Add(CatalogItem.Create
            (
                sku: "CAFE-314",
                description: "1 Pound Guatemalan Coffee Beans",
                unitPrice: 18.80m
            ));

            CartService = new CartService(Repository, new MockLog());
            User = WhenCreateUser();
        }

        public void GivenLoyaltyCard()
        {
            var loyaltyCard = Context.Add(LoyaltyCard.Create
            (
                user: User,
                cardNumber: "35196429541"
            ));
            User.LoyaltyCards.Add(loyaltyCard);
        }

        public Cart WhenLoadCart()
        {
            return CartService.GetCartForUser(User);
        }

        public void WhenAddItemToCart(Cart cart,
            int quantity = 1)
        {
            CartService.AddItemToCart(cart, "CAFE-314", quantity);
            Context.Commit();
        }

        public static CartServiceContext GivenServices()
        {
            return new CartServiceContext();
        }
    }
}
