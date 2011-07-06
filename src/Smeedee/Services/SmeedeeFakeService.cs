using System;
using System.Collections.Generic;
using System.Threading;
using Smeedee.Model;

namespace Smeedee.Services
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
                new Committer("Alex"),
                new Committer("Dag Olav"),
                new Committer("Lars 1"),
                new Committer("Lars 2"),
                new Committer("Børge"),
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
