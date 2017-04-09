using Globalmantics.Logic;
using Highway.Data;

namespace Globalmantics.UnitTests
{
    public class UnitTests
    {
        protected static UserService GivenUserService(IRepository repository)
        {
            return new UserService(repository, new MockLog());
        }
    }
}
