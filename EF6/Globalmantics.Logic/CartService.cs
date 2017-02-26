using Globalmantics.DAL;
using Globalmantics.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Globalmantics.Logic
{
    public class CartService
    {
        private readonly GlobalmanticsContext _context;

        public CartService(GlobalmanticsContext context)
        {
            _context = context;
        }

        public Cart GetCartForUser(User user)
        {
            var cart = _context.Carts
                .Include("CartItems")
                .FirstOrDefault(x => x.UserId == user.UserId);

            if (cart == null)
            {
                cart = _context.Carts.Add(new Cart
                {
                    UserId = user.UserId,
                    CreatedAt = DateTime.Now,
                    CartItems = new List<CartItem>()
                });
            }

            return cart;
        }

        public void AddItemToCart(Cart cart, string sku, int quantity)
        {
            var catalogItem = _context.CatalogItems
                .FirstOrDefault(x => x.Sku == sku);

            cart.CartItems.Add(new CartItem
            {
                CatalogItem = catalogItem,
                Quantity = quantity
            });
        }
    }
}