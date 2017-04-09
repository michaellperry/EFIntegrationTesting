using System;
using Globalmantics.Logic;
using Highway.Data;
using Globalmantics.Domain;
using Globalmantics.DAL;

namespace Globalmantics.IntegrationTests
{
    public class CartTestContext
    {
        public CartService CartService { get; set; }
        public DataContext DataContext { get; set; }
        public UserService UserService { get; set; }
        public string EmailAddress { get; } =
            $"test{Guid.NewGuid().ToString()}@globalmantics.com";

        public User GivenUser()
        {
            var user = UserService.GetUserByEmail(EmailAddress);
            DataContext.Commit();
            return user;
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

        public static CartTestContext GivenServices()
        {
            var configuration = new GlobalmanticsMappingConfiguration();
            var context = new DataContext("GlobalmanticsContext", configuration);
            var repository = new Repository(context);
            var log = new MockLog();
            var userService = new UserService(repository, log);
            var cartService = new CartService(repository, log);

            return new CartTestContext
            {
                DataContext = context,
                UserService = userService,
                CartService = cartService
            };
        }
    }
}
