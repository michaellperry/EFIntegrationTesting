using Highway.Data;

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
        }

        public int UserId { get; private set; }

        public string Email { get; private set; }

        public static User Create(string email)
        {
            return new User
            {
                Email = email
            };
        }
    }
}
