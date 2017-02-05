using Globalmantics.DAL;
using Globalmantics.DAL.Entities;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Globalmantics.Logic
{
    public class ShoppingService
    {
        private GlobalmanticsContext _context;

        public ShoppingService(GlobalmanticsContext context)
        {
            _context = context;
        }

        public Cart GetCurrentCart(int userId)
        {
            var currentCart = _context.Carts
                .Include("CartLines")
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedDateTime)
                .FirstOrDefault();

            if (currentCart == null)
            {
                currentCart = new Cart
                {
                    UserId = userId,
                    CreatedDateTime = DateTime.Now,
                    CartLines = new List<CartLine>()
                };
                _context.Carts.Add(currentCart);
            }

            return currentCart;
        }

        public void AddToCart(int userId, string description, decimal quantity, decimal unitPrice)
        {
            var currentCart = GetCurrentCart(userId);

            currentCart.CartLines.Add(new CartLine
            {
                Description = description,
                Quantity = quantity,
                UnitPrice = unitPrice
            });
        }
    }
}
