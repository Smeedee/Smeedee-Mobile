using System;
using System.Collections.Generic;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests
{
    public class FakeSmeedeeService : ISmeedeeService
    {
        public FakeSmeedeeService()
        {
        }

        public void LoadTopCommiters(Action<AsyncResult<IEnumerable<Commiter>>> callback)
        {
            callback(
                new AsyncResult<IEnumerable<Commiter>>(new [] {
                    new Commiter("John Doe"),
                    new Commiter("Mary Poppins")
                })
            );
        }
    }
}
