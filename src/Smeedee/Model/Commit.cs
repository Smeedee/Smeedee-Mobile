﻿using System;
using Smeedee.Lib;

namespace Smeedee.Model
{
    public class Commit
    {
        public int Revision { get; private set; }
        public string Message { get; private set; }
        public DateTime Date { get; private set; }
        public string User { get; private set; }
		public Uri ImageUri { get; private set; }

        public Commit(string message, DateTime date, string user, Uri uri, int revision)
        {
            Guard.NotNull(message, date, user, uri);
            User = user;
            Date = date;
            Message = message;
			ImageUri = uri;
            Revision = revision;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Commit)) return false;
            var otherCommit = obj as Commit;
            return otherCommit.Message == Message &&
                   otherCommit.Date.Equals(Date) &&
                   otherCommit.User == User &&
				   otherCommit.ImageUri == ImageUri &&
                   otherCommit.Revision == Revision;
        }

        public override int GetHashCode()
        {
            return Message.GetHashCode()*Date.GetHashCode()*User.GetHashCode();
        }
    }
}
