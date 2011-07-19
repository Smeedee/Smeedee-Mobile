using System.Collections.Generic;
using System.Linq;
using Smeedee.Model;

namespace Smeedee.Services.Fakes
{
    public class TopCommittersFakeService : IModelService<TopCommitters>
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
		
        public TopCommitters Get()
        {
            return new TopCommitters();
        }

        public TopCommitters Get(IDictionary<string, string> args)
        {
            var count = (args.ContainsKey("count")) ? int.Parse(args["count"]) : 5;
            var time = (args.ContainsKey("time")) ? int.Parse(args["time"]) : 1;

            var filteredData = data.Take(count);
            return new TopCommitters();
        }
    }
}
