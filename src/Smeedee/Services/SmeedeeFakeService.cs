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

		public void LoadTopCommiters(Action<AsyncResult<IEnumerable<Commiter>>> callback)
		{
			var fakeData = new [] {
				new Commiter("Alex"),
				new Commiter("Dag Olav"),
				new Commiter("Lars 1"),
				new Commiter("Lars 2"),
				new Commiter("Børge"),
			};
			
			BackgroundWorkWithDelay(() => {
				callback(new AsyncResult<IEnumerable<Commiter>>(fakeData));
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
