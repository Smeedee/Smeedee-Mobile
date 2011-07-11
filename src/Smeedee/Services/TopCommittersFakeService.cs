using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Smeedee.Model;

namespace Smeedee.Services
{
    public class TopCommittersFakeService : IModelService<TopCommitters>
    {
        private TopCommitters data = new TopCommitters(new[] {
            new Committer("Lars Kirkholt Melhus", 17, "http://www.foo.com/img.png"),
            new Committer("Dag Olav", 21, "http://www.foo.com/img.png"),
            new Committer("Borge", 16, "http://www.foo.com/img.png"),
            new Committer("Lars Eidnes", 17, "http://www.foo.com/img.png"),
            new Committer("Hans Hauk", 34, "http://www.foo.com/img.png"),
            new Committer("Jens Ulf", 21, "http://www.foo.com/img.png"),
            new Committer("Kari Irak", 0, "http://www.foo.com/img.png"),
        });

        public IEnumerable<TopCommitters> Get(IDictionary<string, string> args)
        {
            return new List<TopCommitters>() { data };
        }

        public TopCommitters GetSingle(IDictionary<string, string> args)
        {
            var count = int.Parse(args["count"]);

            var filteredData = data.Committers.Take(count);
            return new TopCommitters(filteredData.ToList());
        }
    }
}
