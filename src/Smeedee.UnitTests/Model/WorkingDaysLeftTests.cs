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
            SmeedeeApp.Instance.ServiceLocator.Bind<ISmeedeeService>(new WorkingDaysFakeService());
            var model = new WorkingDaysLeft();

            var callbackWasExecuted = false;
            model.Load(() =>
            {
                callbackWasExecuted = true;
            });

            Assert.IsTrue(callbackWasExecuted);
        }
    }


    class WorkingDaysFakeService : ISmeedeeService
    {
        public void GetWorkingDaysLeft(Action<AsyncResult<int>> callback)
        {
            throw new NotImplementedException();
        }

        public void LoadTopCommiters(Action<AsyncResult<IEnumerable<Commiter>>> callback)
        {
            throw new NotImplementedException();
        }
    }

}
