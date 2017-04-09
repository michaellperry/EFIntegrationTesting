using Globalmantics.Logic;
using Highway.Data;

namespace Globalmantics.IntegrationTests
{
    public class IntegrationTests
    {
        protected static UserService GivenUserService(IRepository repository)
        {
            return new UserService(repository, new MockLog());
        }
    }
}
