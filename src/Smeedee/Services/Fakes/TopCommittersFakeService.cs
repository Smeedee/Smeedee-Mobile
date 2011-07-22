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

        private readonly IBackgroundWorker bgWorker;
        private readonly SmeedeeApp app = SmeedeeApp.Instance;

        public TopCommittersFakeService()
        {
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
        }

        private string PretendToGetDataFromHttp(TimePeriod time)
        {
            //In the real implementation, use the TimePeriod in the http call
            var asStrings = data.Select(committer => new[] { committer.Name, committer.Commits.ToString(), committer.ImageUri.ToString() });
            return Csv.ToCsv(asStrings);
        }

        private IEnumerable<Committer> Deserialize(string data)
        {
            return Csv.FromCsv(data).Select(s => new Committer(s[0], int.Parse(s[1]), s[2]));
        }
		
        public void LoadTopCommiters(TimePeriod time, Action<IEnumerable<Committer>> callback)
        {
            bgWorker.Invoke(() => callback(Deserialize(PretendToGetDataFromHttp(time))));
        }
    }
}
