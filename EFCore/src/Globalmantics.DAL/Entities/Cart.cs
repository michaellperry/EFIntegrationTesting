using System;
using System.Collections.Generic;

namespace Globalmantics.DAL.Entities
{
    public class Cart
    {
        public int CartId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
