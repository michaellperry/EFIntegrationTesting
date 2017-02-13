using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globalmantics.DAL.Entities
{
    public class Cart
    {
        public int CartId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
