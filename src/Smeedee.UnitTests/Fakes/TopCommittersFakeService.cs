using System;
using System.Collections.Generic;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests
{
    public class TopCommittersFakeService : ITopCommittersService
    {
        public void LoadTopCommiters(Action<IEnumerable<Committer>> callback)
        {
            callback(
                new [] {
                    new Committer("John Doe"),
                    new Committer("Mary Poppins")
                }
            );
        }
    }
}
