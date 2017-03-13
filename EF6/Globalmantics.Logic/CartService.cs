using Globalmantics.DAL;
using Globalmantics.Domain;
using Globalmantics.Logic.Queries;
using Highway.Data;
using System.Linq;

namespace Globalmantics.Logic
{
    public class CartService
    {
        private readonly IRepository _repository;

        public CartService(IRepository repository)
        {
            _repository = repository;
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

            cart.AddItem(catalogItem, quantity);
        }
    }
}