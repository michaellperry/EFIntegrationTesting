using System;
using Globalmantics.Logic;
using Highway.Data;

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
    }
}
