using System;
using System.Collections.Generic;
using System.Linq;
using Smeedee.Model;

namespace Smeedee.Services.Fakes
{
    public class TopCommittersFakeService : ITopCommittersService
    {
        private Committer[] data = new[] {
            new Committer("Lars Kirkholt Melhus", 17, "http://www.foo.com/img.png"),
            new Committer("Dag Olav", 21, "http://www.foo.com/img.png"),
            new Committer("Borge", 16, "http://www.foo.com/img.png"),
            new Committer("Lars Eidnes", 17, "http://www.foo.com/img.png"),
            new Committer("Hans Hauk", 34, "http://www.foo.com/img.png"),
            new Committer("Jens Ulf", 21, "http://www.foo.com/img.png"),
            new Committer("Kari Irak", 0, "http://www.foo.com/img.png"),
        };
		
        public void LoadTopCommiters(TopCommitters.TimeInterval interval, Action<IEnumerable<Committer>> callback)
        {
            // Disregard interval for now

            callback(data);
        }
    }
}
