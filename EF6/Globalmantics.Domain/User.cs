namespace Globalmantics.Domain
{
    public class User
    {
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
