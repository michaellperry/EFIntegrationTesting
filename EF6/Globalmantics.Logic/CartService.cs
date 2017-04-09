using Globalmantics.DAL;
using Globalmantics.Domain;
using Globalmantics.Logic.Queries;
using Highway.Data;
using System;
using System.Linq;

namespace Globalmantics.Logic
{
    public class CartService
    {
        private readonly IRepository _repository;
        private readonly ILog _log;

        public CartService(IRepository repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public Cart GetCartForUser(User user)
        {
            var cart = _repository.Find(new CartForUser(user.UserId));

            if (cart == null)
            {
                cart = _repository.Context.Add(Cart.Create(user.UserId));
            }

            return cart;
        }

        public void AddItemToCart(Cart cart, string sku, int quantity)
        {
            var catalogItem = _repository.Find(new CatalogItemBySku(sku));

            if (catalogItem == null)
            {
                throw new ArgumentException("Item not found in catalog.", nameof(sku));
            }

            cart.AddItem(catalogItem, quantity);
        }
    }
}