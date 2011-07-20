using System;
using System.Collections.Generic;
using System.Linq;
using Smeedee.Model;
using Smeedee;
using Smeedee.Services;

namespace Smeedee
{
    public class FakeLatestCommitsService : ILatestCommitsService
    {
        public List<Commit> data = new List<Commit>
                                       {
            new Commit("Refactored HerpFactory.Derp()", new DateTime(2011, 7, 7, 12, 0, 0), "larmel"),
            new Commit("Fixed a lot, so this is a really long commit message. In this commit message I have also included several newlines \n\n 1) How will that look? \r\n 2) Should we shorten it? ", new DateTime(2011, 7, 7, 1, 10, 0), "larmel"),
            new Commit("", new DateTime(2011, 7, 6, 2, 0, 0), "larspars"),
            new Commit("Coded new codes.", new DateTime(2011, 7, 6, 1, 0, 0), "dagolap"),
            new Commit("Programmed them programs.", new DateTime(2011, 7, 5, 1, 0, 0), "rodsjo"),
            new Commit("", new DateTime(2011, 7, 5, 2, 0, 0), "rodsjo"),
            new Commit("", new DateTime(2011, 7, 4, 2, 0, 0), "larspars"),
            new Commit("Blabla", new DateTime(2011, 7, 3, 2, 0, 0), "dagolap"),
            new Commit("Changed Smeedee", new DateTime(2011, 7, 2, 2, 0, 0), "alex"),
            new Commit("TopBanner far to left", new DateTime(2011, 7, 2, 2, 0, 0), "alex"),
            new Commit("Flipper working", new DateTime(2011, 7, 1, 2, 0, 0), "rodsjo"),
            new Commit("OnGesture working", new DateTime(2011, 7, 1, 2, 0, 0), "alex"),
            new Commit("Color changes", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Commit("Don't know what to say", new DateTime(2011, 7, 1, 2, 0, 0), "larspars"),
            new Commit("Merged", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Commit("This is number 16", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Commit("This is number 17", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Commit("This is number 18", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Commit("This is number 19", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Commit("This is number 20", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Commit("This is number 21", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Commit("This is number 22", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Commit("This is number 23", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Commit("This is number 24", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Commit("This is number 25", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Commit("This is number 26", new DateTime(2011, 7, 1, 2, 0, 0), "larmel")
                                       };

        private readonly IBackgroundWorker bgWorker;
        private readonly SmeedeeApp app = SmeedeeApp.Instance;

        public FakeLatestCommitsService()
        {
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
        }
        
        public void Get10(int fromIndex, Action<IEnumerable<Commit>> callback)
        {
            bgWorker.Invoke(() => callback(data.Skip(fromIndex).Take(10)));
        }
    }
}
