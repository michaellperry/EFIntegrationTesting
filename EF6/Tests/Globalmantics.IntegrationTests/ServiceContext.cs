using System;
using Globalmantics.Logic;
using Highway.Data;
using Globalmantics.Domain;

namespace Globalmantics.IntegrationTests
{
    public class ServiceContext
    {
        public ServiceContext()
        {
        }
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
    }
}
