using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smeedee.Model;

namespace Smeedee
{
    public class FakeLatestChangesetService : IModelService<Changeset>
    {
        public IEnumerable<Changeset> Get(IDictionary<string, string> args)
        {
            return new[]
                {
                    new Changeset("Refactored HerpFactory.Derp()", new DateTime(2011, 7, 7, 12, 0, 0), "larmel"),
                    new Changeset("Fixed a lot, so this is a really long commit message. In this commit message I have also included several newlines \n\n 1) How will that look? \r\n 2) Should we shorten it? ", new DateTime(2011, 7, 7, 1, 10, 0), "larmel"),
                    new Changeset("", new DateTime(2011, 7, 6, 2, 0, 0), "larspars"),
                    new Changeset("Coded new codes.", new DateTime(2011, 7, 6, 1, 0, 0), "dagolap"),
                    new Changeset("Programmed them programs.", new DateTime(2011, 7, 5, 1, 0, 0), "rodsjo"),
                    new Changeset("Blabla", new DateTime(2011, 7, 7, 2, 0, 0), "larspars")
                };
        }

        public Changeset GetSingle(IDictionary<string, string> args)
        {
            throw new NotImplementedException();
        }
    }
}
