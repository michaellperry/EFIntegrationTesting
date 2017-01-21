using System;
using System.Collections.Generic;

namespace Globalmantics.DAL.Entities
{
    public class Cart
    {
        public int CartId { get; set; }

        public virtual ICollection<CartLine> CartLines { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
