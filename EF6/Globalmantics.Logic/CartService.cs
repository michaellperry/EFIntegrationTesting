using System;
using Globalmantics.DAL;
using Globalmantics.DAL.Entities;

namespace Globalmantics.Logic
{
    public class CartService
    {
        private GlobalmanticsContext context;

        public CartService(GlobalmanticsContext context)
        {
            this.context = context;
        }

        public Cart GetCartForUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}