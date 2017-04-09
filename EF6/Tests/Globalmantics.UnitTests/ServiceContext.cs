using System;
using Globalmantics.Logic;
using Highway.Data.Contexts;

namespace Globalmantics.UnitTests
{
    public class ServiceContext
    {
        public ServiceContext()
        {
        }
        public CartService CartService { get; set; }
        public InMemoryDataContext Context { get; set; }
        public UserService UserService { get; set; }
    }
}
