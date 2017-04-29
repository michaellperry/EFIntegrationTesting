using Highway.Data;
using System.Collections.Generic;

namespace Globalmantics.Domain
{
    public class User : IIdentifiable<int>
    {
        int IIdentifiable<int>.Id
        {
            get { return UserId; }
            set { UserId = value; }
        }

        private User()
        {
            LoyaltyCards = new List<LoyaltyCard>();
        }

        public int UserId { get; private set; }

        public string Email { get; private set; }

        public ICollection<LoyaltyCard> LoyaltyCards { get; }

        public static User Create(string email)
        {
            return new User
            {
                Email = email
            };
        }
    }
}
