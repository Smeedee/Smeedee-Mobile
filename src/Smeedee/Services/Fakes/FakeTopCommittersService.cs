using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Smeedee.Lib;
using Smeedee.Model;

namespace Smeedee.Services.Fakes
{
    public class FakeTopCommittersService : ITopCommittersService
    {
        private Committer[] data = new[] {
            new Committer("Lars Kirkholt Melhus", 17, "http://theme.identi.ca/0.9.7/identica/default-avatar-profile.png"),
            new Committer("Dag Olav", 21, "http://theme.identi.ca/0.9.7/identica/default-avatar-profile.png_ERROR"),
            new Committer("Borge", 7, "http://theme.identi.ca/0.9.7/identica/default-avatar-profile.png"),
            new Committer("Lars Eidnes", 27, "https://secure.gravatar.com/avatar/b7fd9909c31a229d444848d0aa8636ec?s=140&d=https://a248.e.akamai.net/assets.github.com%2Fimages%2Fgravatars%2Fgravatar-140.png"),
            new Committer("Hans Hauk", 34, "http://www.gravatar.com/avatar/a2051ec5f2f820b5dc4567e7fe3080f5"),
            new Committer("Jens Ulf", 21, "http://scienceblogs.com/terrasig/Small%20profile%20avatar.jpg"),
            new Committer("Kari Irak", 1, "http://avatar.hq-picture.com/avatars/img1/angelina_profile_avatar_picture_97617.jpg"),
        };

        private readonly IBackgroundWorker bgWorker;
        private readonly SmeedeeApp app = SmeedeeApp.Instance;

        public FakeTopCommittersService()
        {
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
        }

        private string PretendToGetDataFromHttp(TimePeriod time)
        {
            Thread.Sleep(5000);
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
