using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests.Model
{
    [TestFixture]
    class WorkingDaysLeftTests
    {
        [Test]
        public void Should_invoke_callback_on_load()
        {
            SmeedeeApp.Instance.ServiceLocator.Bind<IWorkingDaysLeftService>(new WorkingDaysLeftFakeService());
            var model = new WorkingDaysLeft();

            var callbackWasExecuted = false;
            model.Load(() =>
            {
                callbackWasExecuted = true;
            });

            Assert.IsTrue(callbackWasExecuted);
        }
    }

    interface IWorkingDaysLeftService
    {
        int GetNumberOfWorkingDaysLeft(Action<AsyncResult<int>> callback);
    }

    class WorkingDaysLeftFakeService : IWorkingDaysLeftService
    {
        public int GetNumberOfWorkingDaysLeft(Action<AsyncResult<int>> callback)
        {
            throw new NotImplementedException();
        }
    }

}
