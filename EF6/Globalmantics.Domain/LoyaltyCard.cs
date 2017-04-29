using Globalmantics.Domain;
using Highway.Data;

namespace Globalmantics.Domain
{
    public class LoyaltyCard : IIdentifiable<int>
    {
        int IIdentifiable<int>.Id
        {
            get { return LoyaltyCardId; }
            set { LoyaltyCardId = value; }
        }

        private LoyaltyCard()
        {
        }

        public int LoyaltyCardId { get; private set; }

        public User User { get; private set; }
        public int UserId { get; private set; }

        public string CardNumber { get; private set; }

        public static LoyaltyCard Create(User user, string cardNumber)
        {
            return new LoyaltyCard
            {
                User = user,
                UserId = user.UserId,
                CardNumber = cardNumber
            };
        }
    }
}
