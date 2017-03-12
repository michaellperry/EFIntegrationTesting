using FluentAssertions;
using Globalmantics.Domain;
using Globalmantics.Logic;
using Highway.Data;
using Highway.Data.Contexts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globalmantics.UnitTests
{
    [TestFixture]
    public class CartServiceTests
    {
        [Test]
        public void CanGetCartWithNoItems()
        {
            var context = new InMemoryDataContext();
            var cartService = new CartService(new Repository(context));

            var cart = cartService.GetCartForUser(User.Create("test@globalmantics.com"));

            cart.CartItems.Count().Should().Be(0);
        }
    }
}
