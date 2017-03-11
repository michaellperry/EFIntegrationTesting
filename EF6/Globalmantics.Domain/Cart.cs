using System;
using System.Collections.Generic;
using System.Linq;

namespace Globalmantics.Domain
{
    public class Cart
    {
        private Cart()
        {
            CartItems = new List<CartItem>();
        }

        public int CartId { get; private set; }

        public User User { get; private set; }
        public int UserId { get; private set; }

        public ICollection<CartItem> CartItems { get; }

        public DateTime CreatedAt { get; private set; }

        public static Cart Create(int userId)
        {
            return new Cart
            {
                UserId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }
}
