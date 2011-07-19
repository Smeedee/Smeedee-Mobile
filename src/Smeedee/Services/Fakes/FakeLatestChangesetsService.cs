using System;
using System.Collections.Generic;
using System.Linq;
using Smeedee.Model;
using Smeedee;
using Smeedee.Services;

namespace Smeedee
{
    public class FakeLatestChangesetsService : ILatestChangesetsService
    {
        private Changeset[] data = new[]
                                       {
            new Changeset("Refactored HerpFactory.Derp()", new DateTime(2011, 7, 7, 12, 0, 0), "larmel"),
            new Changeset("Fixed a lot, so this is a really long commit message. In this commit message I have also included several newlines \n\n 1) How will that look? \r\n 2) Should we shorten it? ", new DateTime(2011, 7, 7, 1, 10, 0), "larmel"),
            new Changeset("", new DateTime(2011, 7, 6, 2, 0, 0), "larspars"),
            new Changeset("Coded new codes.", new DateTime(2011, 7, 6, 1, 0, 0), "dagolap"),
            new Changeset("Programmed them programs.", new DateTime(2011, 7, 5, 1, 0, 0), "rodsjo"),
            new Changeset("", new DateTime(2011, 7, 5, 2, 0, 0), "rodsjo"),
            new Changeset("", new DateTime(2011, 7, 4, 2, 0, 0), "larspars"),
            new Changeset("Blabla", new DateTime(2011, 7, 3, 2, 0, 0), "dagolap"),
            new Changeset("Changed Smeedee", new DateTime(2011, 7, 2, 2, 0, 0), "alex"),
            new Changeset("TopBanner far to left", new DateTime(2011, 7, 2, 2, 0, 0), "alex"),
            new Changeset("Flipper working", new DateTime(2011, 7, 1, 2, 0, 0), "rodsjo"),
            new Changeset("OnGesture working", new DateTime(2011, 7, 1, 2, 0, 0), "alex"),
            new Changeset("Color changes", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Changeset("Don't know what to say", new DateTime(2011, 7, 1, 2, 0, 0), "larspars"),
            new Changeset("Merged", new DateTime(2011, 7, 1, 2, 0, 0), "larmel"),
            new Changeset("This is number 16", new DateTime(2011, 7, 1, 2, 0, 0), "larmel")
                                       };

        private IBackgroundWorker bgWorker;
        private SmeedeeApp app = SmeedeeApp.Instance;

        public FakeLatestChangesetsService()
        {
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
        }

        public void Get(Action<IEnumerable<Changeset>> callback)
        {
            bgWorker.Invoke(() => callback(data));
        }

        public void Get(int count, Action<IEnumerable<Changeset>> callback)
        {
            bgWorker.Invoke(() => callback(data.Take(count)));
        }
    }
}
