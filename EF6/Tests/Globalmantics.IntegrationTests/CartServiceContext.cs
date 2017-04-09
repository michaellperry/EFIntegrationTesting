using Globalmantics.Logic;
using System;
using Highway.Data;
using Globalmantics.Domain;
using Globalmantics.DAL;

namespace Globalmantics.IntegrationTests
{
    public class CartServiceContext
    {
        public CartServiceContext()
        {
        }
        public CartService CartService { get; set; }
        public DataContext Context { get; set; }
        public UserService UserService { get; set; }
        public string EmailAddress { get; } =
            $"test{Guid.NewGuid().ToString()}@globalmantics.com";

        public Cart WhenLoadCart()
        {
            var user = UserService.GetUserByEmail(EmailAddress);
            Context.Commit();

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

        public static CartServiceContext GivenServices()
        {
            var configuration = new GlobalmanticsMappingConfiguration();
            var context = new DataContext("GlobalmanticsContext", configuration);
            var repository = new Repository(context);
            var userService = new UserService(repository);
            var cartService = new CartService(repository, new MockLog());

            var services = new CartServiceContext
            {
                Context = context,
                UserService = userService,
                CartService = cartService
            };
            return services;
        }
    }
}
