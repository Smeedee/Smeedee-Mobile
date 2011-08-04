using System;
using System.Collections.Generic;
using System.Linq;
using Smeedee.Model;

namespace Smeedee.Services.Fakes
{
    public class FakeLatestCommitsService : ILatestCommitsService
    {
		private static Uri uri = new Uri("http://theme.identi.ca/0.9.7/identica/default-avatar-profile.png");
		
        private List<Commit> data = new List<Commit>
            {
            new Commit("Refactored HerpFactory.Derp()", new DateTime(2011, 7, 7, 12, 0, 0), "larmel", uri, 1),
            new Commit("Fixed a lot, so this is a really long commit message. In this commit message I have also included several newlines \n\n 1) How will that look? \r\n 2) Should we shorten it? ", new DateTime(2011, 7, 7, 1, 10, 0), "larmel", uri, 2),
            new Commit("", new DateTime(2011, 7, 6, 2, 0, 0), "larspars", uri, 3),
            new Commit("Coded new codes.", new DateTime(2011, 7, 6, 1, 0, 0), "dagolap", uri, 4),
            new Commit("Programmed them programs.", new DateTime(2011, 7, 5, 1, 0, 0), "rodsjo", uri, 5),
            new Commit("", new DateTime(2011, 7, 5, 2, 0, 0), "rodsjo", uri, 6),
            new Commit("", new DateTime(2011, 7, 4, 2, 0, 0), "larspars", uri, 7),
            new Commit("Blabla", new DateTime(2011, 7, 3, 2, 0, 0), "dagolap", uri, 8),
            new Commit("Changed Smeedee", new DateTime(2011, 7, 2, 2, 0, 0), "alex", uri, 9),
            new Commit("TopBanner far to left", new DateTime(2011, 7, 2, 2, 0, 0), "alex", uri, 10),
            new Commit("Flipper working", new DateTime(2011, 7, 1, 2, 0, 0), "rodsjo", uri, 11),
            new Commit("OnGesture working", new DateTime(2011, 7, 1, 2, 0, 0), "alex", uri, 12),
            new Commit("Color changes", new DateTime(2011, 7, 1, 2, 0, 0), "larmel", uri, 13),
            new Commit("Don't know what to say", new DateTime(2011, 7, 1, 2, 0, 0), "larspars", uri, 14),
            new Commit("Merged", new DateTime(2011, 7, 1, 2, 0, 0), "larmel", uri, 15),
            new Commit("This is number 16", new DateTime(2011, 7, 1, 2, 0, 0), "larmel", uri, 16),
            new Commit("This is number 17", new DateTime(2011, 7, 1, 2, 0, 0), "larmel", uri, 17),
            new Commit("This is number 18", new DateTime(2011, 7, 1, 2, 0, 0), "larmel", uri, 18),
            new Commit("This is number 19", new DateTime(2011, 7, 1, 2, 0, 0), "larmel", uri, 19),
            new Commit("This is number 20", new DateTime(2011, 7, 1, 2, 0, 0), "larmel", uri, 20),
            new Commit("This is number 21", new DateTime(2011, 7, 1, 2, 0, 0), "larmel", uri, 21),
            new Commit("This is number 22", new DateTime(2011, 7, 1, 2, 0, 0), "larmel", uri, 22),
            new Commit("This is number 23", new DateTime(2011, 7, 1, 2, 0, 0), "larmel", uri, 23),
            new Commit("This is number 24", new DateTime(2011, 7, 1, 2, 0, 0), "larmel", uri, 24),
            new Commit("This is number 25", new DateTime(2011, 7, 1, 2, 0, 0), "larmel", uri, 25),
            new Commit("This is number 26", new DateTime(2011, 7, 1, 2, 0, 0), "larmel", uri, 26)
            };

        private readonly IBackgroundWorker bgWorker;

        public FakeLatestCommitsService()
        {
            bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();
        }

        private string PretendToGetDataFromHttp(int fromIndex)
        {
            var subset = data.Skip(fromIndex).Take(10);
            var asStrings = subset.Select(commit => new []
				{ commit.Message, commit.Date.ToString(), commit.User, commit.ImageUri.ToString(), commit.Revision.ToString() }
			);
            return Csv.ToCsv(asStrings);
        }

        private IEnumerable<Commit> Deserialize(string data)
        {
            return Csv.FromCsv(data).Select(s => 
				new Commit(s[0], DateTime.Parse(s[1]), s[2], new Uri(s[3]), int.Parse(s[4])));
        }
        
        public void Get10AfterRevision(int fromIndex, Action<IEnumerable<Commit>> callback)
        {
            bgWorker.Invoke(() => {
				System.Threading.Thread.Sleep(1500);
				callback(Deserialize(PretendToGetDataFromHttp(fromIndex)));
			});
        }

        public void Get10Latest(Action<IEnumerable<Commit>> callback)
        {
            bgWorker.Invoke(() => {
                System.Threading.Thread.Sleep(1500);
				callback(Deserialize(PretendToGetDataFromHttp(0)));
			});
        }
    }
}
