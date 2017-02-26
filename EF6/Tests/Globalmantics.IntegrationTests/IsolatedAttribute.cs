using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Transactions;

namespace Globalmantics.IntegrationTests
{
    public class IsolatedAttribute : Attribute, ITestAction
    {
        private TransactionScope _scope;
        public ActionTargets Targets => ActionTargets.Test;

        public void BeforeTest(ITest test)
        {
            _scope = new TransactionScope(TransactionScopeOption.RequiresNew);
        }

        public void AfterTest(ITest test)
        {
            if (_scope != null)
                _scope.Dispose();
            _scope = null;
        }
    }
}
