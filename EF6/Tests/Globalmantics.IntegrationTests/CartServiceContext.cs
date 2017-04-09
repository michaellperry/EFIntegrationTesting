using Globalmantics.Logic;
using System;
using Highway.Data;

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
    }
}
