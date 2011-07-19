using System;
using System.Collections.Generic;
using Smeedee.Model;
using Smeedee;

namespace Smeedee.UnitTests
{
    public class TopCommittersFakeService : ITopCommittersService
    {
        public void LoadTopCommiters(Action<IEnumerable<Committer>> callback)
        {
            callback(
                new [] {
                    new Committer("John Doe", 16, "http://www.foo.com/doe.png"),
                    new Committer("Steven Bonnell", 17, "http://www.foo.com/doe.png"),
                    new Committer("Josh Wash", 9, "http://www.foo.com/doe.png"),
                    new Committer("Luke Walker", 20, "http://www.foo.com/doe.png"),
                    new Committer("Lilly Lucid", 16, "http://www.foo.com/doe.png"),
                    new Committer("Frank", 16, "http://www.foo.com/doe.png"),
                    new Committer("Carl", 16, "http://www.foo.com/doe.png"),
                    new Committer("The Dude", 16, "http://www.foo.com/doe.png"),
                    new Committer("Dude Dudeman", 16, "http://www.foo.com/doe.png"),
                    new Committer("Neil Nail", 16, "http://www.foo.com/doe.png"),
                    new Committer("Headless Nick", 16, "http://www.foo.com/doe.png"),
                    new Committer("Mary Poppins", 16, "http://www.foo.com/mary.png")
                }
            );
        }
    }
}
