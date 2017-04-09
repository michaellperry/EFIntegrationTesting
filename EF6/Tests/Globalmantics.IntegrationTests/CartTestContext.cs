using Globalmantics.Domain;
using Globalmantics.Logic;
using Highway.Data;

namespace Globalmantics.IntegrationTests
{
    public class CartTestContext : UserTestContext
    {
        public CartService CartService { get; set; }

        protected CartTestContext()
        {
            CartService = new CartService(Repository, Log);
        }

        public Cart WhenLoadCart()
        {
            var user = GivenUser();

            var cart = CartService.GetCartForUser(user);
            DataContext.SaveChanges();
            return cart;
        }

        public void WhenAddItemToCart(Cart cart, string sku = "CAFE-314", int quantity = 1)
        {
            CartService.AddItemToCart(cart, sku, quantity);
            DataContext.SaveChanges();
        }

        public static new CartTestContext GivenServices()
        {
            return new CartTestContext();
        }
    }
}
