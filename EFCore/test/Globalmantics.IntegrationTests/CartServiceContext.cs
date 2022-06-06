using Globalmantics.Logic;
using Globalmantics.Domain;

namespace Globalmantics.IntegrationTests
{
    public class CartServiceContext : UserServiceContext
    {
        public CartService CartService { get; }

        protected CartServiceContext()
        {
            CartService = new CartService(Context);
        }

        public Cart WhenLoadCart()
        {
            var user = UserService.GetUserByEmail(EmailAddress);
            Context.SaveChanges();

            var cart = CartService.GetCartForUser(user);
            Context.SaveChanges();
            return cart;
        }

        public void WhenAddItemToCart(Cart cart,
            int quantity = 1)
        {
            CartService.AddItemToCart(cart, "CAFE-314", quantity);
            Context.SaveChanges();
        }

        public static new CartServiceContext GivenServices()
        {
            return new CartServiceContext();
        }
    }
}
