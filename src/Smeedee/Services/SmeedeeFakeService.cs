using System;
using System.Collections.Generic;
using System.Threading;
using Smeedee.Model;

namespace Smeedee
{
    public class SmeedeeFakeService : ISmeedeeService
    {
        public SmeedeeFakeService()
        {
        }
        
        private int fakeDelay = 2000;
        public int FakeDelayInMilliseconds
        {
            get { return fakeDelay; }
            set { fakeDelay = value; }
        }

        public void LoadTopCommiters(Action<AsyncResult<IEnumerable<Committer>>> callback)
        {
            var fakeData = new [] {
                new Committer("Alex", 16, "http://www.foo.com/alex.png"),
                new Committer("Dag Olav", 16, "http://www.foo.com/dagolap.png"),
                new Committer("Lars 1", 16, "http://www.foo.com/l1.png"),
                new Committer("Lars 2", 16, "http://www.foo.com/l2.png"),
                new Committer("Børge", 16, "http://www.foo.com/borge.png"),
            };
            
            BackgroundWorkWithDelay(() => {
                callback(new AsyncResult<IEnumerable<Committer>>(fakeData));
            });
        }
        
        private void BackgroundWorkWithDelay(Action callback)
        {
            ThreadPool.QueueUserWorkItem(o => {
                Thread.Sleep(fakeDelay);
                callback();
            });
        }
    }
}
