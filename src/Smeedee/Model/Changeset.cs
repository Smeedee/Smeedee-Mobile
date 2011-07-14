using System;

namespace Smeedee.Model
{
    public class Changeset
    {
        public string Message { get; private set; }
        public DateTime Date { get; private set; }
        public string User { get; private set; }

        public Changeset(string message, DateTime date, string user)
        {
            Guard.NotNull(message, date, user);
            User = user;
            Date = date;
            Message = message;
        }
    }
}
