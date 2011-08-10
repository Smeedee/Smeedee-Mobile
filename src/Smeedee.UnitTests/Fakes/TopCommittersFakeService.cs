using System;
using System.Collections.Generic;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests.Fakes
{
    public class TopCommittersFakeService : ITopCommittersService
    {
        public Committer[] PastDayData = new[]
                                         {
                                             new Committer("John Doe", 16, "http://www.foo.com/doe.png"),
                                             new Committer("Steven Bonnell", 42, "http://www.foo.com/doe.png"),
                                             new Committer("Josh Wash", 9, "http://www.foo.com/doe.png"),
                                             new Committer("Luke Walker", 20, "http://www.foo.com/doe.png"),
                                             new Committer("Lilly Lucid", 16, "http://www.foo.com/doe.png"),
                                             new Committer("Frank", 41, "http://www.foo.com/doe.png"),
                                             new Committer("Carl", 26, "http://www.foo.com/doe.png"),
                                             new Committer("The Dude", 34, "http://www.foo.com/doe.png"),
                                             new Committer("Dude Dudeman", 35, "http://www.foo.com/doe.png"),
                                             new Committer("Neil Nail", 12, "http://www.foo.com/doe.png"),
                                             new Committer("Headless Nick", 1, "http://www.foo.com/doe.png"),
                                             new Committer("Mary Poppins", 0, "http://www.foo.com/mary.png")
                                         };

        public Committer[] DefaultData = new[]
                                         {
                                             new Committer("John Doe", 16, "http://www.foo.com/doe.png"),
                                             new Committer("Steven Bonnell", 42, "http://www.foo.com/doe.png"),
                                             new Committer("Josh Wash", 9, "http://www.foo.com/doe.png"),
                                             new Committer("Luke Walker", 20, "http://www.foo.com/doe.png"),
                                             new Committer("Lilly Lucid", 16, "http://www.foo.com/doe.png"),
                                             new Committer("Frank", 45, "http://www.foo.com/doe.png"),
                                             new Committer("Carl", 26, "http://www.foo.com/doe.png"),
                                             new Committer("The Dude", 62, "http://www.foo.com/doe.png"),
                                             new Committer("Dude Dudeman", 47, "http://www.foo.com/doe.png"),
                                             new Committer("Neil Nail", 12, "http://www.foo.com/doe.png"),
                                             new Committer("Headless Nick", 1, "http://www.foo.com/doe.png"),
                                             new Committer("Mary Poppins", 0, "http://www.foo.com/mary.png")
                                         };
        public void LoadTopCommiters(TimePeriod time, Action<IEnumerable<Committer>> callback)
        {
            switch (time)
            {
                case TimePeriod.PastDay:
                    callback(PastDayData);
                    break;
                default:
                    callback(DefaultData);
                    break;
            }
        }
    }
}
