using Globalmantics.DAL;
using Globalmantics.Domain;
using Microsoft.EntityFrameworkCore;
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
            var cart = _context.Cart
                .Include(x => x.CartItems)
                .FirstOrDefault(x => x.UserId == user.UserId);

            if (cart == null)
            {
                cart = _context.Cart.Add(Cart.Create(user.UserId)).Entity;
            }

            return cart;
        }

        public void AddItemToCart(Cart cart, string sku, int quantity)
        {
            var catalogItem = _context.CatalogItem
                .FirstOrDefault(x => x.Sku == sku);

            var cartItem = CartItem.Create(catalogItem);
            cartItem.IncreaseQuantity(quantity);
            cart.CartItems.Add(cartItem);
        }
    }
}