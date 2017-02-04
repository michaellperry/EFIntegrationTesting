using Globalmantics.DAL;
using Globalmantics.DAL.Entities;
using System.Linq;
using System;

namespace Globalmantics.Logic
{
    public class ShoppingService
    {
        private GlobalmanticsContext _context;

        public ShoppingService(GlobalmanticsContext context)
        {
            _context = context;
        }

        public Cart GetCurrentCart(User user)
        {
            var currentCart = _context.Carts
                .Where(c => c.User == user)
                .OrderByDescending(c => c.CreatedDateTime)
                .FirstOrDefault();

            if (currentCart == null)
            {
                currentCart = new Cart
                {
                    User = user
                };
            }

            return currentCart;
        }

        public void AddToCart(User user, string description, decimal quantity, decimal unitPrice)
        {
            var currentCart = GetCurrentCart(user);

            currentCart.CartLines.Add(new CartLine
            {
                Description = description,
                Quantity = quantity,
                UnitPrice = unitPrice
            });
        }
    }
}
