using FluentAssertions;
using Globalmantics.DAL;
using Globalmantics.DAL.Entities;
using Globalmantics.Logic;
using NUnit.Framework;
using System.Linq;

namespace Globalmantics.IntegrationTests
{
    [TestFixture]
    public class ShoppingServiceTests
    {
        [Test]
        public void CanAddToCart()
        {
            var context = new GlobalmanticsContext();

            var user = new User();
            context.Users.Add(user);
            context.SaveChanges();

            var shoppingService = new ShoppingService(context);

            shoppingService.AddToCart(user.UserId, "Widgets", 3, 4.98m);
            context.SaveChanges();

            var currentCart = shoppingService.GetCurrentCart(user.UserId);
            var lines = currentCart.CartLines
                .Select(cl => $"{cl.Description}: {cl.Quantity}@{cl.UnitPrice}")
                .ToArray();

            string.Join("\n", lines).Should().Be("Widgets: 3@4.98");
        }
    }
}
