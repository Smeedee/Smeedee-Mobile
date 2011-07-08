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
        });

        public IEnumerable<TopCommitters> Get()
        {
            return new List<TopCommitters>() { data };
        }

        public TopCommitters GetSingle()
        {
            return data;
        }
    }
}
