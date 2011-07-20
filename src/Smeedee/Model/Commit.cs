using System;

namespace Smeedee.Model
{
    public class Commit
    {
        public string Message { get; private set; }
        public DateTime Date { get; private set; }
        public string User { get; private set; }

        public Commit(string message, DateTime date, string user)
        {
            Guard.NotNull(message, date, user);
            User = user;
            Date = date;
            Message = message;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Commit)) return false;
            var otherCommit = obj as Commit;
            return otherCommit.Message == this.Message &&
                   otherCommit.Date.Equals(this.Date) &&
                   otherCommit.User == this.User;
        }
    }
}
